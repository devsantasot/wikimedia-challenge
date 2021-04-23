using DS_ProgramingChallengeLibrary.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace DS_ProgramingChallengeLibrary
{
    public class UrlSystem : IUrlSystem
    {
        private readonly ILogger<UrlSystem> _log;
        private readonly IConfiguration _config;

        public UrlSystem(ILogger<UrlSystem> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public List<DownloadRequestModel> GetUrlList(int lastHoursRequest)
        {
            _log.LogInformation("Building Urls...");
            DateTime dateTimeFileName = DateTime.Now;
            List<DownloadRequestModel> urls = new List<DownloadRequestModel>();

            for (int fileHourIndex = 0; fileHourIndex < lastHoursRequest; fileHourIndex++)
            {
                urls.Add(BuildUrlRequest(dateTimeFileName, fileHourIndex));
            }

            _log.LogInformation("Building Urls done.");
            return urls;
        }

        private DownloadRequestModel BuildUrlRequest(DateTime dateTimeFileName, int fileHourIndex)
        {
            string baseURL = _config.GetValue<string>("BaseURLDownload");
            string fileRuteFormat = _config.GetValue<string>("FileRuteFormat");
            string fileNameFormat = _config.GetValue<string>("FileNameFormat");
            string workspacePath = _config.GetValue<string>("FilesWorkspacePath");
            dateTimeFileName = dateTimeFileName.AddHours(-1 * fileHourIndex);

            DownloadRequestModel downloadRequestModel = new();
            downloadRequestModel.FileName = dateTimeFileName.ToString(fileNameFormat);
            downloadRequestModel.Address = new Uri($"{baseURL}/{dateTimeFileName.ToString(fileRuteFormat)}/{downloadRequestModel.FileName}");
            downloadRequestModel.FilesWorkspacePath = $"{(string.IsNullOrEmpty(workspacePath) ? AppContext.BaseDirectory : workspacePath)}/Pending";

            FileHelper.CreatePathIfNotExist(downloadRequestModel.FilesWorkspacePath);

            return downloadRequestModel;
        }
    }
}
