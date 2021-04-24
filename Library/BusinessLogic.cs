using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DS_ProgramingChallengeLibrary
{
    /// <summary>
    /// I've use TPL Dataflow.
    /// 1. Download the file to workspace (local disk).
    ///     1.1. Get the URLs to download.
    ///     1.2. Download the resource.
    /// 2. Decompress the file  to workspace (local disk).
    /// 3. Process all data in a unify file.
    ///     3.1. Read the file and transform the data to reduce the size. 
    ///     3.2. Unify all processed files into one (máx 500MB).
    ///     3.3. Process all data using the unify file.
    /// 4. Print the result of the analysis.
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

            TransformManyBlock<int, DownloadRequestModel> listUrlDownloadBlock = new(_unitOfWork.UrlSystem.GetUrlList, settings);
            TransformBlock<DownloadRequestModel, string> downloadFilesBlock = new(_unitOfWork.DownloadHandler.DownloadData, settings);
            TransformBlock<string, string> decompressFileBlok = new(_unitOfWork.DecompressorHandler.DecompressFile);
            TransformBlock<string, string> fileParserBlock = new(_unitOfWork.TransformFileData.TransformDataAsync, settings);
            BatchBlock<string> batchBlock = new(lastHoursRequest);
            TransformBlock<IEnumerable<string>, string> unionFilesBlock = new(_unitOfWork.FileSystem.CombineMultipleTextFiles, settings);
            TransformBlock<string, IEnumerable<OutputModel>> resultParserBlock = new(_unitOfWork.ProcessFileData.ProcessDataAsync, settings);
            ActionBlock<IEnumerable<OutputModel>> outputBlock = new(_unitOfWork.OutputResultParser.ShowResult, settings);

            DataflowLinkOptions linkOptions = new() { PropagateCompletion = true };

            listUrlDownloadBlock.LinkTo(downloadFilesBlock, linkOptions);
            downloadFilesBlock.LinkTo(decompressFileBlok, linkOptions);
            decompressFileBlok.LinkTo(fileParserBlock, linkOptions);
            fileParserBlock.LinkTo(batchBlock, linkOptions);
            batchBlock.LinkTo(unionFilesBlock, linkOptions);
            unionFilesBlock.LinkTo(resultParserBlock, linkOptions);
            resultParserBlock.LinkTo(outputBlock, linkOptions);

            await listUrlDownloadBlock.SendAsync(lastHoursRequest);
            listUrlDownloadBlock.Complete();

            await outputBlock.Completion;
        }
    }
}
