
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DS_ProgramingChallengeLibrary.Helper;
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

        public void DownloadData()
        {

            DateTime dateTimeFileName = DateTime.Now;
            int lastHoursRequest = _config.GetValue<int>("LastHoursRequest");

            DownloadURLs downloadURLs = new DownloadURLs(_config);
            List<DownloadRequestModel> urls = downloadURLs.GetList(dateTimeFileName, lastHoursRequest);

            var urlTasks = urls.Select((downloadInfo, index) =>
            {
                _log.LogInformation("Downloading Data from {uri} (chunk #{index}) ", downloadInfo.Address.AbsoluteUri, index);
                return DownloadHelper.DownloadFileTaskAsync(downloadInfo.Address, downloadInfo.FileNamePath);
            });

            Task.WaitAll(urlTasks.ToArray());
            Console.WriteLine("Done.");

        }
    }
}
