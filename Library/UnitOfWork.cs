using Microsoft.Extensions.Configuration;

namespace DS_ProgramingChallengeLibrary
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConfiguration configuration,
                          IDownloadHandler downloadHandler,
                          IFileAnalysis fileParser,
                          IFileSystem fileSystem,
                          IDecompressorHandler decompressorHandler,
                          IUrlSystem urlSystem,
                          IOutputResultParser outputResultParser)
        {
            Configuration = configuration;
            DownloadHandler = downloadHandler;
            UrlSystem = urlSystem;
            DecompressorHandler = decompressorHandler;
            FileParser = fileParser;
            FileSystem = fileSystem;
            OutputResultParser = outputResultParser;
        }

        public IConfiguration Configuration { get; private set; }

        public IDownloadHandler DownloadHandler { get; private set; }

        public IUrlSystem UrlSystem { get; private set; }

        public IDecompressorHandler DecompressorHandler { get; private set; }

        public IFileAnalysis FileParser { get; private set; }

        public IFileSystem FileSystem { get; private set; }

        public IOutputResultParser OutputResultParser { get; private set; }
    }
}
