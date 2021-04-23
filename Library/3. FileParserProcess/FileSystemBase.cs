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
    public abstract class FileSystemBase : IFileSystem
    {
        protected readonly ILogger _log;
        protected readonly IConfiguration _config;

        public FileSystemBase(ILogger<FileSystemBase> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public string CombineMultipleTextFiles(IEnumerable<string> inputFiles)
        {
            const int chunkSize = 2 * 1024; // 2KB
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string fileName = "output.txt";
            string resultFileNamePath = $"{resultFilePath}/{fileName}";

            _log.LogInformation("Unifying files into {fileNamePath}", resultFileNamePath);

            FileHelper.CreatePathIfNotExist(resultFilePath);
            FileHelper.DeleteFileIfExist(resultFileNamePath);

            try
            {
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
                    }
                }
            }
            finally
            {
                GC.Collect();
            }

            return resultFileNamePath;
        }

        public abstract Task<string> SaveDataAsync(IEnumerable<DataModelSummary> resultGroupBy, string fileNamePath);
        
    }
}