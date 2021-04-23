using DS_ProgramingChallengeLibrary.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;

namespace DS_ProgramingChallengeLibrary
{
    public class DecompressorHandler : IDecompressorHandler
    {
        private readonly ILogger _log;

        public DecompressorHandler(ILogger<DecompressorHandler> log)
        {
            _log = log;
        }

        public string DecompressFile(string fileNamePath)
        {
            _log.LogInformation("Decompressing data: {0}", fileNamePath);
            string newFileNamePath = string.Empty;
            FileInfo fileToDecompress = new(fileNamePath);
            try
            {
                if (fileToDecompress.Exists)
                {
                    using (FileStream originalFileStream = fileToDecompress.OpenRead())
                    {
                        string currentFileName = fileToDecompress.FullName;
                        newFileNamePath = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                        using FileStream decompressedFileStream = File.Create(newFileNamePath);
                        using GZipStream decompressionStream = new(originalFileStream, CompressionMode.Decompress);
                        decompressionStream.CopyTo(decompressedFileStream);
                    }

                    fileToDecompress.Delete();

                    _log.LogInformation("Decompressing data finished.");
                }
            }
            finally
            {
                GC.Collect();
            }
            return newFileNamePath;
        }
    }
}
