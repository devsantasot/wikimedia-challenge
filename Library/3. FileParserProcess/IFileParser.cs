using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IFileParser
    {
        void TransformDataIntoDataTable(out DataTable resultDataTable);
        Task<GroupByOutputModel> TransformDataByChunks(string newfileNamePath);
        Task<string> TransformData(string newfileNamePath);
    }
}