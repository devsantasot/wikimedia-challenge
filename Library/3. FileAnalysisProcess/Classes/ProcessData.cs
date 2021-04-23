
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public class ProcessData : FileAnalysisBase, IProcessData
    {
        public ProcessData(ILogger<ProcessData> log, IFileSystem fileSystem)
        : base(log, fileSystem)
        {
        }

        public Task<IEnumerable<OutputModel>> ProcessDataAsync(string fileNamePath)
        {
            _log.LogInformation("Reading and processing final data: {fileNamePath}", fileNamePath);
            var separator = new char[0];
            List<DataModelSummary> dataModel;
            IEnumerable<OutputModel> outputModel;

            try
            {
                lock (this)
                {
                    dataModel = GetDataModelSumFromFile(fileNamePath, separator);

                    _log.LogInformation("Reading data finished.");
                }

                var resultGroupBySum = GroupBySumData(dataModel);
                var resultGroupByMax = GroupByMaxData(resultGroupBySum);

                outputModel = from r in resultGroupByMax
                              join g in resultGroupBySum on new { r.domain_code, r.count_views } equals new { g.domain_code, g.count_views }
                              select new OutputModel
                              {
                                  domain_code = r.domain_code,
                                  page_title = g.page_title,
                                  max_count_views = r.count_views
                              };

                _log.LogInformation("Processing data finished.");
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
    }
}
