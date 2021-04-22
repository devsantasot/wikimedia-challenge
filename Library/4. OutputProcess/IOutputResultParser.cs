using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;
using System.Data;

namespace DS_ProgramingChallengeLibrary
{
    public interface IOutputResultParser
    {
        void ShowResultInConsole(DataTable resultDataTable);
        void ShowResult(IEnumerable<GroupByOutputModel> obj);
    }
}