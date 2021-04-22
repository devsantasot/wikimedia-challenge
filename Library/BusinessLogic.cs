using DS_ProgramingChallengeLibrary.Helpers;
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DS_ProgramingChallengeLibrary
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _log;
        private readonly IConfiguration _config;
        private readonly IDownloadHandler _downloadHandler;
        private readonly IFileParser _fileParser;
        private readonly IDecompressorHandler _decompressorHandler;
        private readonly IUrlSystem _urlSystem;
        private readonly IOutputResultParser _outputResultParser;

        public BusinessLogic(ILogger<BusinessLogic> log,
                          IConfiguration config,
                          IDownloadHandler downloadHandler,                          
                          IFileParser fileParser,
                          IDecompressorHandler decompressorHandler,
                          IUrlSystem urlSystem, 
                          IOutputResultParser outputResultParser)
        {
            _log = log;
            _config = config;
            _downloadHandler = downloadHandler;
            _fileParser = fileParser;
            _decompressorHandler = decompressorHandler;
            _urlSystem = urlSystem;
            _outputResultParser = outputResultParser;
        }

        public void DownloadAndProcessData(out DataTable resultDataTable)
        {
            int lastHoursRequest = _config.GetValue<int>("LastHoursRequest");
            _log.LogInformation("Processing Data from the last {lastHoursRequest} hours...", lastHoursRequest);

            List<DownloadRequestModel> urls = _urlSystem.GetUrlList(lastHoursRequest);
            _downloadHandler.DownloadData(urls);
            _decompressorHandler.DecompressFiles();
            //_fileParser.TransformDataIntoDataTable(out resultDataTable);
            resultDataTable = new DataTable();
        }

        public async Task ProcesingAsync()
        {
            var lastHoursRequest = _config.GetValue<int>("LastHoursRequest");
            _log.LogInformation("Processing Data from the last {lastHoursRequest} hours...", lastHoursRequest);

            var settings = new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 2,
            };

            var listUrlDownloadBlock = new TransformManyBlock<int, DownloadRequestModel>(_urlSystem.GetUrlList, settings);
            var downloadFilesBlock = new TransformBlock<DownloadRequestModel, string>(_downloadHandler.DownloadData, settings);
            var decompressFileBlok = new TransformBlock<string, string>(_decompressorHandler.DecompressFile);
            var fileParserBlock = new TransformBlock<string, string>(_fileParser.TransformData, settings);
            var batchBlock = new BatchBlock<string>(lastHoursRequest);
            var outputBlock = new ActionBlock<IEnumerable<string>>(_outputResultParser.ShowResult, settings);

            DataflowLinkOptions linkOptions = new DataflowLinkOptions() { PropagateCompletion = true };

            listUrlDownloadBlock.LinkTo(downloadFilesBlock, linkOptions);
            downloadFilesBlock.LinkTo(decompressFileBlok, linkOptions);
            decompressFileBlok.LinkTo(fileParserBlock, linkOptions);
            fileParserBlock.LinkTo(batchBlock, linkOptions);
            batchBlock.LinkTo(outputBlock, linkOptions);

            await listUrlDownloadBlock.SendAsync(lastHoursRequest);
            listUrlDownloadBlock.Complete();

            await outputBlock.Completion;
        }
    }
}
