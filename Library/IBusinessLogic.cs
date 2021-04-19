using System.Data;

namespace DS_ProgramingChallengeLibrary
{
    public interface IBusinessLogic
    {
        void DownloadAndProcessData(out DataTable resultDataTable);
    }
}