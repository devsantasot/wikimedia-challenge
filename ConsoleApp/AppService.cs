using DS_ProgramingChallengeLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// This project use Dependency Injection, Serilog and Settings

namespace ConsoleApp
{
    public class AppService : IAppService
    {
        private readonly IDownloadHandler _downloadHandler;
        private readonly IDecompressorHandler _decompressorHandler;
        private readonly IFileParser _fileParser;
        private readonly IOutputResultParser _resultParser;

        public AppService(IDownloadHandler downloadHandler,
                          IDecompressorHandler decompressorHandler,
                          IFileParser fileParser,
                          IOutputResultParser resultParser)
        {
            _downloadHandler = downloadHandler;
            _decompressorHandler = decompressorHandler;
            _fileParser = fileParser;
            _resultParser = resultParser;
        }

        public void Run()
        {
            _downloadHandler.DownloadData();
            _decompressorHandler.DecompressDownloadedFiles();
            _fileParser.TransformDataIntoDataTable();
            _resultParser.ShowResultInConsole();
        }
    }
}
