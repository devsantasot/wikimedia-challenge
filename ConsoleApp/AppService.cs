using DS_ProgramingChallengeLibrary;
using System.Threading.Tasks;

// This project use Dependency Injection, Serilog and Settings

namespace ConsoleApp
{
    public class AppService : IAppService
    {
        private readonly IBusinessLogic _businessLogic;

        public AppService(IBusinessLogic dataHandler)
        {
            _businessLogic = dataHandler;
        }


        public async Task RunAsync()
        {
            await _businessLogic.ProcesingAsync();
        }
    }
}
