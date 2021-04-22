
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_ProgramingChallengeLibrary.Helpers;
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
        public void DecompressFiles()
        {
            string fileDownloadPath = GeneralHelper.GetDownloadedFilesPath(_config);
            foreach (var fileNamePath in Directory.GetFiles(fileDownloadPath))
            {
                _log.LogInformation("Decompressing Data: {0}", fileNamePath);
                DecompressHelper.DecompressFile(fileNamePath, true);
                _log.LogInformation("Decompressed: {0}", fileNamePath);
            }      
        }

        public string DecompressFile(string fileNamePath)
        {
            _log.LogInformation("Decompressing data: {0}", fileNamePath);
            DecompressHelper.DecompressFile(fileNamePath, out string newfileNamePath);
            _log.LogInformation("Decompressing data finished.");
            return newfileNamePath;
        }
    }
}
