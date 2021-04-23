
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_ProgramingChallengeLibrary.Helpers;
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DS_ProgramingChallengeLibrary
{
    public class FileParser : IFileParser
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;
        private readonly IFileSystem _fileSystem;

        public FileParser(ILogger<FileParser> log, IConfiguration config, IFileSystem fileSystem)
        {
            _log = log;
            _config = config;
            _fileSystem = fileSystem;
        }

        public async Task<string> TransformDataAsync(string fileNamePath)
        {
            _log.LogInformation("Transforming data: {fileNamePath}", fileNamePath);
            var separator = new char[0]; // or white space ' ' 
            List<ContainedDataModel> containedData = new();
            IEnumerable<ContainedDataModel> preResultData;
            string fileName = string.Empty;

            try
            {
                lock (this)
                {
                    using (FileStream fs = File.Open(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        fileName = fs.Name;
                        using (BufferedStream bs = new BufferedStream(fs))
                        using (StreamReader sr = new StreamReader(bs))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                var columns = line.Split(separator);

                                string domain_code = columns[0];
                                string page_title = columns[1];
                                if (int.TryParse(columns[2], out int count_views) == false)
                                {
                                    foreach (var item in columns)
                                    {
                                        if (int.TryParse(item, out count_views))
                                            break;
                                    }
                                }

                                containedData.Add(new ContainedDataModel()
                                {
                                    domain_code = domain_code,
                                    page_title = page_title,
                                    count_views = count_views
                                });
                            }
                        }
                    }

                    preResultData = GroupByCountData(containedData);

                    _log.LogInformation("Transforming data finished.");
                }

                await _fileSystem.SaveDataAsync(preResultData, fileName);
            }
            finally
            {
                GC.Collect();
            }
            
            return fileName;
           
        }

        public void TransformDataIntoDataTable(out DataTable resultDataTable)
        {
            _log.LogInformation("Transforming Data");
            string fileDownloadPath = GeneralHelper.GetDownloadedFilesPath(_config);
            string resultFilePath = GeneralHelper.GetResultFilePath(_config);
            string resultFileNamePath = FileParserHelper.CombineMultipleTextFiles(fileDownloadPath, resultFilePath, "output.txt", true);
            resultDataTable = FileParserHelper.ConvertToDataTable(resultFileNamePath, 4, ' ');
            _log.LogInformation("Transformed.");
        }

        private IEnumerable<ContainedDataModel> GroupByCountData(List<ContainedDataModel> containedData)
        {
            //return containedData
            //    .GroupBy(c => new { c.domain_code, c.page_title })
            //    //.Where(grp => grp.Count() > 1)
            //    .Select(gb => new ContainedDataModel()
            //    {
            //        domain_code = gb.Key.domain_code,
            //        page_title = gb.Key.page_title,
            //        count_views = gb.Count()
            //    });

            return from e in containedData
                   group e by new { e.domain_code, e.page_title } into gb
                   //where gb.Count() > 1
                   select new ContainedDataModel
                   {
                       domain_code = gb.Key.domain_code,
                       page_title = gb.Key.page_title,
                       count_views = gb.Count()
                   };
        }

        private List<ContainedDataModel> GroupBySumData(List<ContainedDataModel> containedData)
        {
            return containedData
               .GroupBy(c => new { c.domain_code, c.page_title })
               .Select(gb => new ContainedDataModel()
               {
                   domain_code = gb.Key.domain_code,
                   page_title = gb.Key.page_title,
                   count_views = gb.Sum(x => x.count_views)
               }).ToList();
        }
    }
}
