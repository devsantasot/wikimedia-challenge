using System;

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
