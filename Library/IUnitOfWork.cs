using Microsoft.Extensions.Configuration;

namespace DS_ProgramingChallengeLibrary
{
    public interface IUnitOfWork
    {
        IConfiguration Configuration { get; }
        IDownloadHandler DownloadHandler { get; }
        IUrlSystem UrlSystem { get; }
        IDecompressorHandler DecompressorHandler { get; }
        IFileAnalysis FileParser { get; }
        IFileSystem FileSystem { get; }
        IOutputResultParser OutputResultParser { get; }
    }
}
