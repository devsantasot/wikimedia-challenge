using System;
using System.Collections.Generic;

namespace DS_ProgramingChallengeLibrary
{
    public interface IUrlSystem
    {
        List<DownloadRequestModel> GetUrlList(DateTime dateTimeFileName, int lastHoursRequest);

        List<DownloadRequestModel> GetUrlList(int lastHoursRequest);
    }
}