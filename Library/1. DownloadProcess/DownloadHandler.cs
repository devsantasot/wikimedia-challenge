using DS_ProgramingChallengeLibrary.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace DS_ProgramingChallengeLibrary
{
    public class DownloadHandler : IDownloadHandler
    {
        private readonly ILogger _log;

        public DownloadHandler(ILogger<DownloadHandler> log)
        {
            _log = log;
        }

        public string DownloadData(DownloadRequestModel downloadInfo)
        {
            _log.LogInformation("Downloading from {address} to {location}", downloadInfo.Address, downloadInfo.FileNamePath);
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(downloadInfo.Address, downloadInfo.FileNamePath);
                _log.LogInformation("Download finished.");
            }
            finally
            {
                GC.Collect();
            }

            return downloadInfo.FileNamePath;
        }
    }
}
