using DS_ProgramingChallengeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DS_ProgramingChallengeLibrary.Helpers;

namespace DS_ProgramingChallengeLibrary
{
    public class FileSystem : IFileSystem
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public FileSystem(ILogger<FileSystem> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public async Task SaveData(List<ContainedDataModel> resultGroupBy, string fileNamePath)
        {
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string fileName = Path.GetFileName(fileNamePath);
            string resultFileNamePath = $"{resultFilePath}/{fileName}";
            _log.LogInformation("Saving result data: {fileNamePath}", resultFileNamePath);

            using FileStream createStream = File.Create(resultFileNamePath);
            await JsonSerializer.SerializeAsync(createStream, resultGroupBy);
        }
    }
}
