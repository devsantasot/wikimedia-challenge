using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IUnitOfWork
    {
        IConfiguration Configuration { get; }
        IDownloadHandler DownloadHandler { get; }
        IUrlSystem UrlSystem { get; }
        IDecompressorHandler DecompressorHandler { get; }
        IFileParser FileParser { get; }
        IFileSystem FileSystem { get; }
        IOutputResultParser OutputResultParser { get; }        
    }
}
