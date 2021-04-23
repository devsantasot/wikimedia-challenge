using System;
using System.IO;

namespace DS_ProgramingChallengeLibrary.Helpers
{
    public class FileHelper
    {
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
