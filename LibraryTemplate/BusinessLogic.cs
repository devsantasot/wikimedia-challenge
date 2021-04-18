
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LibraryTemplate
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public BusinessLogic(ILogger<BusinessLogic> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void ProcessData()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                // Test - Demo
                _log.LogInformation("Run number {runNumber}", i);
            }
        }
    }
}
