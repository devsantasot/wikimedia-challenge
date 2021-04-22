using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IDownloadHandler
    {
        void DownloadData(List<DownloadRequestModel> urls);
        string DownloadData(DownloadRequestModel downloadRequestModel);
    }
}