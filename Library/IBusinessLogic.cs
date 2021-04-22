using System.Data;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IBusinessLogic
    {
        void DownloadAndProcessData(out DataTable resultDataTable);

        Task ProcesingAsync();
    }
}