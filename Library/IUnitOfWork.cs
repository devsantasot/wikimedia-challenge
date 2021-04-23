using Microsoft.Extensions.Configuration;

namespace DS_ProgramingChallengeLibrary
{
    public interface IUnitOfWork
    {
        IConfiguration Configuration { get; }
        IDownloadHandler DownloadHandler { get; }
        IUrlSystem UrlSystem { get; }
        IDecompressorHandler DecompressorHandler { get; }
        IProcessData ProcessFileData { get; }
        ITransformData TransformFileData { get; }
        IFileSystem FileSystem { get; }
        IOutputResultParser OutputResultParser { get; }
    }
}
