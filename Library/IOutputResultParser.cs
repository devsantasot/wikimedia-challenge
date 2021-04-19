using System.Data;

namespace DS_ProgramingChallengeLibrary
{
    public interface IOutputResultParser
    {
        void ShowResultInConsole(DataTable resultDataTable);
    }
}