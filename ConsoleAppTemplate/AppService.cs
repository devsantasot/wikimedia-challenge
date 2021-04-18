using LibraryTemplate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// This project use Dependency Injection, Serilog, Settings

namespace ConsoleAppTemplate
{
    public class AppService : IAppService
    {
        private readonly IBusinessLogic _businessLogic;

        public AppService(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        public void Run()
        {
            _businessLogic.ProcessData();
        }
    }
}
