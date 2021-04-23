
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
            var separator = new char[0];
            List<DataModel> dataModel;
            string fileName = string.Empty;

            lock (this)
            {
                dataModel = GetDataModelFromFile(fileNamePath, separator);

                _log.LogInformation("Transforming data finished.");
            }

            await _fileSystem.SaveDataAsync(dataModel, fileNamePath);

            return fileName;

        }

        public Task<IEnumerable<OutputModel>> CountDataAsync(string fileNamePath)
        {
            _log.LogInformation("Transforming data: {fileNamePath}", fileNamePath);
            var separator = new char[0];
            List<DataModel> dataModel;
            IEnumerable<OutputModel> outputModel;
            string fileName = string.Empty;

            try
            {
                lock (this)
                {
                    dataModel = GetDataModelFromFile(fileNamePath, separator);

                    _log.LogInformation("Transforming data finished.");
                }

                var resultGroupByCount = GroupByCountData(dataModel);
                var resultGroupByMax = GroupByMaxData(resultGroupByCount);

                outputModel = from r in resultGroupByMax
                              join g in resultGroupByCount on new { r.domain_code, r.count } equals new { g.domain_code, g.count }
                              select new OutputModel
                              {
                                  domain_code = r.domain_code,
                                  page_title = g.page_title,
                                  max_count_views = r.count
                              };
            }
            finally
            {
                GC.Collect();
            }

            return Task.Run(() =>
            {
                return outputModel;
            });
        }

        private static List<DataModel> GetDataModelFromFile(string fileNamePath, char[] separator)
        {
            List<DataModel> dataModel = new();
            //string fileName;
            try
            {
                using (FileStream fs = File.Open(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    //fileName = fs.Name;
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var columns = line.Split(separator);

                            string domain_code = columns[0];
                            string page_title = columns[1];

                            dataModel.Add(new DataModel()
                            {
                                domain_code = domain_code,
                                page_title = page_title
                            });
                        }
                    }
                }
            }
            finally
            {
                GC.Collect();
            }

            return dataModel;
        }

        private IEnumerable<ContainedDataModel> GroupByCountData(List<DataModel> dataModel)
        {
            return from e in dataModel
                   group e by new { e.domain_code, e.page_title } into gb
                   select new ContainedDataModel
                   {
                       domain_code = gb.Key.domain_code,
                       page_title = gb.Key.page_title,
                       count = gb.Count()
                   };
        }

        private IEnumerable<ContainedDataModel> GroupByMaxData(IEnumerable<ContainedDataModel> containedData)
        {
            return containedData
               .GroupBy(c => new { c.domain_code })
               .Select(gb => new ContainedDataModel()
               {
                   domain_code = gb.Key.domain_code,
                   count = gb.Max(x => x.count)
               })
               .OrderByDescending(i => i.count)
               .Take(100);
        }
    }
}
