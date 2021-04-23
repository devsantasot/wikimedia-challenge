namespace DS_ProgramingChallengeLibrary
{
    public interface IDownloadHandler
    {
        /// <summary>
        /// Download resources from a specified URL
        /// </summary>
        /// <param name="downloadRequestModel"></param>
        /// <returns>File Name of the downloaded resource</returns>
        string DownloadData(DownloadRequestModel downloadRequestModel);
    }
}