using System;

namespace DS_ProgramingChallengeLibrary
{
    public interface IDownloadHandler
    {
        void DownloadData(DateTime dateTimeFileName, int fileHourIndex);
    }
}