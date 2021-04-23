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
    public class FileSystemJson : FileSystemBase
    {

        public FileSystemJson(ILogger<FileSystemJson> log, IConfiguration config) 
            : base(log, config)
        {
        }

        public override async Task SaveDataAsync(IEnumerable<ContainedDataModel> resultGroupBy, string fileNamePath)
        {
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string fileName = Path.GetFileName(fileNamePath);
            string resultFileNamePath = $"{resultFilePath}/{fileName}";
            try
            {
                FileParserHelper.CreatePathIfNotExist(resultFilePath);
                _log.LogInformation("Saving result data [json]: {fileNamePath}", resultFileNamePath);

                using FileStream createStream = File.Create(resultFileNamePath);
                await JsonSerializer.SerializeAsync(createStream, resultGroupBy);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
