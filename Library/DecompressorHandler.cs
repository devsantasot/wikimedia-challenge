
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DS_ProgramingChallengeLibrary
{
    public class DecompressorHandler : IDecompressorHandler
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public DecompressorHandler(ILogger<DecompressorHandler> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void DecompressDownloadedFiles()
        {

            _log.LogInformation("Decompressing Data");

            _log.LogInformation("Decompressed.");
        }
    }
}
