using DS_ProgramingChallengeLibrary.Helpers;
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public class TransformData : FileAnalysisBase, ITransformData
    {
        public TransformData(ILogger<TransformData> log, IFileSystem fileSystem)
        : base(log, fileSystem)
        {
        }

        public Task<string> TransformDataAsync(string fileNamePath)
        {
            _log.LogInformation("Transforming data: {fileNamePath}", fileNamePath);
            var separator = new char[0];
            IEnumerable<DataModelSummary> dataModel;
            IEnumerable<DataModelSummary> dataModelSum;

            lock (this)
            {
                dataModel = GetDataModelSumFromFile(fileNamePath, separator);
                dataModelSum = GroupBySumData(dataModel);
                _log.LogInformation("Transforming data finished.");
            }

            return _fileSystem.SaveDataAsync(dataModelSum, fileNamePath);
        }
    }
}
