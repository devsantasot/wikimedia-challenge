
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
            //for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            //{
            //    // Test - Demo
            //    _log.LogInformation("Run number {runNumber}", i);
            //}

            _log.LogInformation("Downloading Data");

            _log.LogInformation("Downloaded.");
        }
    }
}
