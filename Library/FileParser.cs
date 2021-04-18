
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void TransformDataIntoDataTable()
        {

            _log.LogInformation("Transforming Data");

            _log.LogInformation("Transformed.");
        }

    }
}
