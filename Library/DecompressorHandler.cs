
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DS_ProgramingChallengeLibrary
{
    public class DecompressorHandler : IDecompressorHandler
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public DecompressorHandler(ILogger<DecompressorHandler> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void DecompressFile(string FileNamePath)
        {
            _log.LogInformation("Decompressing Data");

            FileInfo fileToDecompress = new FileInfo(FileNamePath);

            if (fileToDecompress.Exists)
            {
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            _log.LogInformation("Decompressed: {0}", fileToDecompress.Name);
                        }
                    }
                }

                fileToDecompress.Delete();
            }            
        }
    }
}
