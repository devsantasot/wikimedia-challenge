using DS_ProgramingChallengeLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

// This project use Dependency Injection, Serilog and Settings

namespace ConsoleApp
{
    public class AppService : IAppService
    {
        private readonly IBusinessLogic _dataHandler;
        private readonly IOutputResultParser _resultParser;

        public AppService(IBusinessLogic dataHandler,
                          IOutputResultParser resultParser)
        {
            _dataHandler = dataHandler;
            _resultParser = resultParser;
        }

        public void Run()
        {
            _dataHandler.DownloadAndProcessData(out DataTable resultDataTable);
            _resultParser.ShowResultInConsole(resultDataTable);
        }
    }
}
