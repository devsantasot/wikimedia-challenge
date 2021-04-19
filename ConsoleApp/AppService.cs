using DS_ProgramingChallengeLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// This project use Dependency Injection, Serilog and Settings

namespace ConsoleApp
{
    public class AppService : IAppService
    {
        private readonly IDataHandler _dataHandler;
        private readonly IOutputResultParser _resultParser;

        public AppService(IDataHandler dataHandler,
                          IOutputResultParser resultParser)
        {
            _dataHandler = dataHandler;
            _resultParser = resultParser;
        }

        public void Run()
        {
            _dataHandler.DownloadAndProcessData();
            _resultParser.ShowResultInConsole();
        }
    }
}
