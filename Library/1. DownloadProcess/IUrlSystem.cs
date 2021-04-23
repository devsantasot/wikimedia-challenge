using System.Collections.Generic;

namespace DS_ProgramingChallengeLibrary
{
    public interface IUrlSystem
    {
        /// <summary>
        /// Get the URLs for download
        /// </summary>
        /// <param name="lastHoursRequest">Numbers of hours</param>
        /// <returns>A list of DownloadRequestModel object</returns>
        List<DownloadRequestModel> GetUrlList(int lastHoursRequest);
    }
}