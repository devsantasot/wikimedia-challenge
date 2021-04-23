using System;
using System.Net;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helpers
{
    public static class DownloadHelper
    {
        public static Task DownloadFileAsync(Uri address, string fileNamePath)
        {
            WebClient wc = new WebClient();
            var downloadTask = wc.DownloadFileTaskAsync(address, fileNamePath);

            return downloadTask;
        }

        public static void DownloadFile(Uri address, string fileNamePath)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(address, fileNamePath);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
