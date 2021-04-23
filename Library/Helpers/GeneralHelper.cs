using Microsoft.Extensions.Configuration;
using System;

namespace DS_ProgramingChallengeLibrary.Helpers
{
    public static class GeneralHelper
    {
        public static string GetResultFilePath(IConfiguration config)
        {
            string filePath = config.GetValue<string>("FilesWorkspacePath");
            string resultFilePath = $"{(string.IsNullOrEmpty(filePath) ? AppContext.BaseDirectory : filePath)}/Result";

            return resultFilePath;
        }
    }
}
