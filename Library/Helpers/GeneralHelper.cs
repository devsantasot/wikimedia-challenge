using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helpers
{
    public static class GeneralHelper
    {
        public static string GetDownloadedFilesPath(IConfiguration config)
        {
            string filePath = config.GetValue<string>("FilesWorkspacePath");
            string fileDownloadPath = $"{(string.IsNullOrEmpty(filePath) ? AppContext.BaseDirectory : filePath)}/Pending";

            return fileDownloadPath;
        }

        public static string GetResultFilePath(IConfiguration config)
        {
            string filePath = config.GetValue<string>("FilesWorkspacePath");
            string resultFilePath = $"{(string.IsNullOrEmpty(filePath) ? AppContext.BaseDirectory : filePath)}/Result";

            return resultFilePath;
        }
    }
}
