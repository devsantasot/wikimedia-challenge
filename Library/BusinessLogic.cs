using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DS_ProgramingChallengeLibrary
{
    /// <summary>
    /// I've use TPL Dataflow.
    /// 1. Get the URLs to download.
    /// 2. Download the file to workspace (local disk).
    /// 3. Decompress the file  to workspace (local disk).
    /// 4. Read the file and transform the data to reduce the size. 
    /// 5. Unify all processed files into one.
    /// 6. Process all data in the unify file.
    /// 7. Print the result of the analysis.
    /// </summary>
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _log;
        private readonly IUnitOfWork _unitOfWork;

        public BusinessLogic(ILogger<BusinessLogic> log, IUnitOfWork unitOfWork)
        {
            _log = log;
            _unitOfWork = unitOfWork;
        }

        public async Task ProcesingAsync()
        {
            var lastHoursRequest = _unitOfWork.Configuration.GetValue<int>("LastHoursRequest");
            _log.LogInformation("Processing Data from the last {lastHoursRequest} hours...", lastHoursRequest);

            var settings = new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 1,
            };

            var listUrlDownloadBlock = new TransformManyBlock<int, DownloadRequestModel>(_unitOfWork.UrlSystem.GetUrlList, settings);
            var downloadFilesBlock = new TransformBlock<DownloadRequestModel, string>(_unitOfWork.DownloadHandler.DownloadData, settings);
            var decompressFileBlok = new TransformBlock<string, string>(_unitOfWork.DecompressorHandler.DecompressFile);
            var fileParserBlock = new TransformBlock<string, string>(_unitOfWork.TransformFileData.TransformDataAsync, settings);
            var batchBlock = new BatchBlock<string>(lastHoursRequest);
            var unionFileParserBlock = new TransformBlock<IEnumerable<string>, string>(_unitOfWork.FileSystem.CombineMultipleTextFiles, settings);
            var resultParserBlock = new TransformBlock<string, IEnumerable<OutputModel>>(_unitOfWork.ProcessFileData.ProcessDataAsync, settings);
            var outputBlock = new ActionBlock<IEnumerable<OutputModel>>(_unitOfWork.OutputResultParser.ShowResult, settings);

            DataflowLinkOptions linkOptions = new DataflowLinkOptions() { PropagateCompletion = true };

            listUrlDownloadBlock.LinkTo(downloadFilesBlock, linkOptions);
            downloadFilesBlock.LinkTo(decompressFileBlok, linkOptions);
            decompressFileBlok.LinkTo(fileParserBlock, linkOptions);
            fileParserBlock.LinkTo(batchBlock, linkOptions);
            batchBlock.LinkTo(unionFileParserBlock, linkOptions);
            unionFileParserBlock.LinkTo(resultParserBlock, linkOptions);
            resultParserBlock.LinkTo(outputBlock, linkOptions);

            await listUrlDownloadBlock.SendAsync(lastHoursRequest);
            listUrlDownloadBlock.Complete();

            await outputBlock.Completion;
        }
    }
}
