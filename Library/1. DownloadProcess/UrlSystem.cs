using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class UrlSystem : IUrlSystem
    {
        private readonly ILogger<UrlSystem> _log;
        private readonly IConfiguration _config;

        public UrlSystem(ILogger<UrlSystem> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public List<DownloadRequestModel> GetUrlList(DateTime dateTimeFileName, int lastHoursRequest)
        {
            List<DownloadRequestModel> urls = new List<DownloadRequestModel>();

            for (int fileHourIndex = 0; fileHourIndex < lastHoursRequest; fileHourIndex++)
            {
                urls.Add(BuildUrlRequest(dateTimeFileName, fileHourIndex));
            }

            return urls;
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
            string filePath = _config.GetValue<string>("FilesWorkspacePath");
            dateTimeFileName = dateTimeFileName.AddHours(-1 * fileHourIndex);

            DownloadRequestModel downloadRequestModel = new DownloadRequestModel();

            downloadRequestModel.FileName = dateTimeFileName.ToString(fileNameFormat);
            downloadRequestModel.Address = new Uri($"{baseURL}/{dateTimeFileName.ToString(fileRuteFormat)}/{downloadRequestModel.FileName}");
            downloadRequestModel.FilesWorkspacePath = $"{(string.IsNullOrEmpty(filePath) ? AppContext.BaseDirectory : filePath)}/Pending";

            if (Directory.Exists(downloadRequestModel.FilesWorkspacePath) == false)
            {
                Directory.CreateDirectory(downloadRequestModel.FilesWorkspacePath);
            }

            return downloadRequestModel;
        }
    }
}
