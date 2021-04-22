using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public class DownloadRequestModel
    {
        public Uri Address { get; set; }
        public string FileName { get; set; }
        public string FilesWorkspacePath { get; set; }
        public string FileNamePath
        {
            get
            {
                return $"{FilesWorkspacePath}/{FileName}";
            }
        }
    }
}
