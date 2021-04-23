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

        public override async Task<string> SaveDataAsync(IEnumerable<DataModelSummary> resultGroupBy, string fileNamePath)
        {
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string fileName = Path.GetFileName(fileNamePath);
            string resultFileNamePath = $"{resultFilePath}/{fileName}";
            try
            {
                FileHelper.CreatePathIfNotExist(resultFilePath);
                _log.LogInformation("Saving result data: {fileNamePath}", resultFileNamePath);

                using StreamWriter sw = new(resultFileNamePath);
                foreach (DataModelSummary item in resultGroupBy)
                {
                    await sw.WriteLineAsync($"{item.domain_code} {item.page_title} {item.count_views}");
                }
            }
            finally
            {
                GC.Collect();
            }
            return resultFileNamePath;
        }        
    }
}
