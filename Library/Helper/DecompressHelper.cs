﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helper
{
    public static class DecompressHelper
    {
        public static void DecompressFile(string FileNamePath, bool deleteCompressFile = false)
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
    }
}