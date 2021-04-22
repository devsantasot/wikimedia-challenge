using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helpers
{
    public static class DecompressHelper
    {
        public static void DecompressFile(string FileNamePath, bool deleteCompressFile = true)
        {
            //_log.LogInformation("Decompressing Data: {0}", FileNamePath);

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
                            //_log.LogInformation("Decompressed: {0}", fileToDecompress.Name);
                        }
                    }
                }

                if (deleteCompressFile)
                {
                    fileToDecompress.Delete();
                }
            }
        }

        public static void DecompressFile(string fileNamePath, out string newFileNamePath, bool deleteCompressFile = true)
        {
            //_log.LogInformation("Decompressing Data: {0}", FileNamePath);
            newFileNamePath = string.Empty;
            FileInfo fileToDecompress = new FileInfo(fileNamePath);

            if (fileToDecompress.Exists)
            {
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    newFileNamePath = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using (FileStream decompressedFileStream = File.Create(newFileNamePath))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            //_log.LogInformation("Decompressed: {0}", fileToDecompress.Name);
                        }
                    }
                }

                if (deleteCompressFile)
                {
                    fileToDecompress.Delete();
                }
            }
        }
    }
}
