using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IFileSystem
    {
        Task<string> SaveDataAsync(IEnumerable<DataModelSummary> resultGroupBy, string fileName);

        string CombineMultipleTextFiles(IEnumerable<string> inputFiles);
    }
}