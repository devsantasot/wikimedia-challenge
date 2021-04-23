using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helpers
{
    public class FileHelper
    {
        public static string CombineMultipleTextFiles(string mainPath, string resultPath = "", string resultNameFile = "output.txt", bool deleteOtherFiles = false)
        {
            string resultFileNamePath;
            const int chunkSize = 2 * 1024; // 2KB
            var inputFiles = Directory.GetFiles(mainPath);

            resultPath = $"{(string.IsNullOrEmpty(resultPath) ? AppContext.BaseDirectory : resultPath)}";
            resultFileNamePath = $"{resultPath}/{resultNameFile}";

            CreatePathIfNotExist(resultPath);
            DeleteFileIfExist(resultFileNamePath);

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

                    if (deleteOtherFiles)
                    {
                        DeleteFileIfExist(file);
                    }
                }
            }

            return resultFileNamePath;
        }

        public static void DeleteFileIfExist(string fileNamePath)
        {
            if (Directory.Exists(fileNamePath))
            {
                Directory.Delete(fileNamePath);
            }
        }

        public static void CreatePathIfNotExist(string filePath)
        {
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
        }

        public static void DeleteAllFiles(string directoryPath)
        {
            DirectoryInfo di = new(directoryPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
