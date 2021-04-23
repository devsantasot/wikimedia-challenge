using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IDownloadHandler
    {
        string DownloadData(DownloadRequestModel downloadRequestModel);
    }
}