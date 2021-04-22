﻿
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

        public void DownloadData(List<DownloadRequestModel> urls)
        {
            DateTime dateTimeFileName = DateTime.Now;
            int lastHoursRequest = _config.GetValue<int>("LastHoursRequest");

            //UrlSystem downloadURLs = new UrlSystem(_log, _config);
            //List<DownloadRequestModel> urls = downloadURLs.GetUrlList(dateTimeFileName, lastHoursRequest);

            var urlTasks = urls.Select((downloadInfo, index) =>
            {
                _log.LogInformation("Downloading Data from {uri} (chunk #{index}) ", downloadInfo.Address.AbsoluteUri, index);
                return DownloadHelper.DownloadFileAsync(downloadInfo.Address, downloadInfo.FileNamePath);
            });

            Task.WaitAll(urlTasks.ToArray());
            Console.WriteLine("Done.");
        }

        public string DownloadData(DownloadRequestModel downloadInfo)
        {
            _log.LogInformation("Downloading from {address} to {location}", downloadInfo.Address, downloadInfo.FileNamePath);
            DownloadHelper.DownloadFile(downloadInfo.Address, downloadInfo.FileNamePath);
            return downloadInfo.FileNamePath;
            _log.LogInformation("Downloading from {address} finished.");
        }
    }
}