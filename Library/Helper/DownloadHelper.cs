using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Helper
{
    public class DownloadHelper
    {
        public DownloadHelper(IConfiguration _config, DateTime dateTimeFileName, int fileHourIndex)
        {
            string baseURL = _config.GetValue<string>("BaseURLDownload");
            string fileRuteFormat = _config.GetValue<string>("FileRuteFormat");
            string fileNameFormat = _config.GetValue<string>("FileNameFormat");
            string filePath = _config.GetValue<string>("FileDownloadPath");
            dateTimeFileName = dateTimeFileName.AddHours(-1 * fileHourIndex);

            FileName = dateTimeFileName.ToString(fileNameFormat);
            Address = new Uri($"{baseURL}/{dateTimeFileName.ToString(fileRuteFormat)}/{FileName}");
            FileDownloadPath = $"{(string.IsNullOrEmpty(filePath) ? AppContext.BaseDirectory : filePath)}/Pending";

            if (Directory.Exists(FileDownloadPath) == false)
            {
                Directory.CreateDirectory(FileDownloadPath);
            }
        }

        public Uri Address { get; set; }
        public string FileName { get; private set; }
        public string FileDownloadPath { get; private set; }
        public string FileNamePath
        {
            get
            {
                return $"{FileDownloadPath}/{FileName}";              
            }
        }
    }
}
