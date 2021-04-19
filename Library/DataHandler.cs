using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public class DataHandler : IDataHandler
    {
        private readonly IDownloadHandler _downloadHandler;
        private readonly IFileParser _fileParser;
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public DataHandler(IDownloadHandler downloadHandler,                          
                          IFileParser fileParser,
                          ILogger<DataHandler> log, 
                          IConfiguration config)
        {
            _downloadHandler = downloadHandler;
            _fileParser = fileParser;
            _log = log;
            _config = config;
        }

        public void DownloadAndProcessData()
        {
            int lastHoursRequest = _config.GetValue<int>("LastHoursRequest");
            DateTime dateTimeFileName = DateTime.Now;

            _log.LogInformation("Processing Data from the last {lastHoursRequest} hours...", lastHoursRequest);
            for (int fileHourIndex = 0; fileHourIndex < lastHoursRequest; fileHourIndex++)
            {
                _downloadHandler.DownloadData(dateTimeFileName, fileHourIndex);                
                _fileParser.TransformDataIntoDataTable();
            }
            _log.LogInformation("Downloaded.");
        }
    }
}
