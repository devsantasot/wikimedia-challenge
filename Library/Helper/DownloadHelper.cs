using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helper
{
    public static class DownloadHelper
    {
        public static Task DownloadFileTaskAsync(Uri address, string fileNamePath)
        {
            WebClient wc = new WebClient();
            var downloadTask = wc.DownloadFileTaskAsync(address, fileNamePath);

            return downloadTask;
        }
    }
}
