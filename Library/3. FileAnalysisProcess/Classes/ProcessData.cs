
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

                    _log.LogInformation("Reading data finished. Processing data started...");
                }

                var resultGroupBySum = GroupBySumData(dataModel).ToList();
                var resultGroupByMax = GroupByMaxData(resultGroupBySum).ToList();

                outputModel = GetOutputModel(resultGroupBySum, resultGroupByMax);

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
