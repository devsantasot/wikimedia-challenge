using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IFileSystem
    {
        /// <summary>
        /// Save Data to a file
        /// </summary>
        /// <param name="resultGroupBy">Model to save</param>
        /// <param name="fileName">File name of the new file</param>
        /// <returns>Absolute Path of the new file</returns>
        Task<string> SaveDataAsync(IEnumerable<DataModelSummary> resultGroupBy, string fileName);
        /// <summary>
        /// Merge file into one.
        /// </summary>
        /// <param name="inputFiles">Files to merge</param>
        /// <returns>Absolute Path of the new file</returns>
        string CombineMultipleTextFiles(IEnumerable<string> inputFiles);
    }
}