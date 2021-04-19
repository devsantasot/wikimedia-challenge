
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
        private readonly IDecompressorHandler _decompressorHandler;

        public DownloadHandler(ILogger<DownloadHandler> log, IConfiguration config, IDecompressorHandler decompressorHandler)
        {
            _log = log;
            _config = config;
            _decompressorHandler = decompressorHandler;
        }

        public void DownloadData(DateTime dateTimeFileName, int fileHourIndex)
        {
            DownloadHelper downloadHelper = new DownloadHelper(_config, dateTimeFileName, fileHourIndex);
            using (WebClient client = new WebClient())
            {
                _log.LogInformation("Downloading Data from {uri} (chunk #{index}) ", downloadHelper.Address.AbsoluteUri, fileHourIndex);
                client.DownloadFile(downloadHelper.Address, downloadHelper.FileNamePath);
            }
            _log.LogInformation("Chunk #{index} downloaded.", fileHourIndex);

            _decompressorHandler.DecompressFile(downloadHelper.FileNamePath);
        }
    }
}
