using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger<BusinessLogic> _log;
        private readonly IConfiguration _config;
        private readonly IDownloadHandler _downloadHandler;
        private readonly IFileParser _fileParser;
        private readonly IDecompressorHandler _decompressorHandler;

        public BusinessLogic(ILogger<BusinessLogic> log,
                          IConfiguration config,
                          IDownloadHandler downloadHandler,                          
                          IFileParser fileParser,
                          IDecompressorHandler decompressorHandler)
        {
            _log = log;
            _config = config;
            _downloadHandler = downloadHandler;
            _fileParser = fileParser;
            _decompressorHandler = decompressorHandler;
        }

        public void DownloadAndProcessData(out DataTable resultDataTable)
        {
            int lastHoursRequest = _config.GetValue<int>("LastHoursRequest");
            _log.LogInformation("Processing Data from the last {lastHoursRequest} hours...", lastHoursRequest);

            _downloadHandler.DownloadData();
            _decompressorHandler.DecompressFiles();
            _fileParser.TransformDataIntoDataTable(out resultDataTable);
        }
    }
}
