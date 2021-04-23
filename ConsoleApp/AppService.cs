using DS_ProgramingChallengeLibrary;
using System.Threading.Tasks;

// This project use Dependency Injection, Serilog and Settings

namespace ConsoleApp
{
    public class AppService : IAppService
    {
        private readonly IBusinessLogic _businessLogic;
        private readonly IOutputResultParser _resultParser;

        public AppService(IBusinessLogic dataHandler,
                          IOutputResultParser resultParser)
        {
            _businessLogic = dataHandler;
            _resultParser = resultParser;
        }


        public async Task RunAsync()
        {
            await _businessLogic.ProcesingAsync();
        }
    }
}
