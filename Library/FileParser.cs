
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_ProgramingChallengeLibrary.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DS_ProgramingChallengeLibrary
{
    public class FileParser : IFileParser
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public FileParser(ILogger<FileParser> log, IConfiguration config)
        {
            _log = log;
            _config = config;
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
    }
}
