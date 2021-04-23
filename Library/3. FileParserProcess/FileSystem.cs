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
    public class FileSystem : FileSystemBase
    {

        public FileSystem(ILogger<FileSystem> log, IConfiguration config)
            : base(log, config)
        {
        }

        public override async Task SaveDataAsync(IEnumerable<DataModel> resultGroupBy, string fileNamePath)
        {
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string fileName = Path.GetFileName(fileNamePath);
            string resultFileNamePath = $"{resultFilePath}/{fileName}";
            try
            {
                FileParserHelper.CreatePathIfNotExist(resultFilePath);
                _log.LogInformation("Saving result data: {fileNamePath}", resultFileNamePath);

                using StreamWriter sw = new(resultFileNamePath);
                foreach (DataModel item in resultGroupBy)
                {
                    await sw.WriteLineAsync($"{item.domain_code} {item.page_title}");
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        public override string CombineMultipleTextFiles(IEnumerable<string> inputFiles)
        {
            const int chunkSize = 2 * 1024; // 2KB
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string fileName = "output.txt";
            string resultFileNamePath = $"{resultFilePath}/{fileName}";

            FileParserHelper.CreatePathIfNotExist(resultFilePath);
            FileParserHelper.DeleteFileIfExist(resultFileNamePath);

            using (var output = File.Create(resultFileNamePath))
            {
                foreach (var file in inputFiles)
                {
                    using (var input = File.OpenRead(file))
                    {
                        var buffer = new byte[chunkSize];
                        int bytesRead;
                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    }

                    FileParserHelper.DeleteFileIfExist(file);
                }
            }

            return resultFileNamePath;
        }
    }
}
