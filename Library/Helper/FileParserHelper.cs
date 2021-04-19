using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helper
{
    public static class FileParserHelper
    {
        public static DataTable ConvertToDataTable(string filePath, int numberOfColumns, char separator)
        {
            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                var cols = line.Split(separator);

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < 3; cIndex++)
                {
                    dr[cIndex] = cols[cIndex];
                }

                tbl.Rows.Add(dr);
            }

            return tbl;
        }

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

        private static void DeleteFileIfExist(string fileNamePath)
        {
            if(Directory.Exists(fileNamePath))
            {
                Directory.Delete(fileNamePath);
            }
        }

        private static void CreatePathIfNotExist(string filePath)
        {
            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
        }
    }
}
