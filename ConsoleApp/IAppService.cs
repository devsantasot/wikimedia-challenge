using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IAppService
    {
        void Run();

        Task RunAsync();
    }
}