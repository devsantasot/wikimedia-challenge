
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DS_ProgramingChallengeLibrary.Helpers;
using System.Net;

namespace DS_ProgramingChallengeLibrary
{
    public class DownloadHandler : IDownloadHandler
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public DownloadHandler(ILogger<DownloadHandler> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public string DownloadData(DownloadRequestModel downloadInfo)
        {
            _log.LogInformation("Downloading from {address} to {location}", downloadInfo.Address, downloadInfo.FileNamePath);            
            DownloadHelper.DownloadFile(downloadInfo.Address, downloadInfo.FileNamePath);
            _log.LogInformation("Download finished.");
            
            return downloadInfo.FileNamePath;
        }
    }
}
