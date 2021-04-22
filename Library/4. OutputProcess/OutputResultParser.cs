
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace DS_ProgramingChallengeLibrary
{
    public class OutputResultParser : IOutputResultParser
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public OutputResultParser(ILogger<OutputResultParser> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void ShowResult(IEnumerable<string> obj)
        {
            _log.LogInformation("Showing result");
            _log.LogInformation(obj.Count().ToString());
        }

        public void ShowResultInConsole(DataTable resultDataTable)
        {
            _log.LogInformation("Building and showing result...");
            _log.LogInformation(resultDataTable.Rows.Count.ToString());
        }
    }
}
