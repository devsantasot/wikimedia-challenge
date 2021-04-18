
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void ShowResultInConsole()
        {

            _log.LogInformation("Showing result");

        }
    }
}
