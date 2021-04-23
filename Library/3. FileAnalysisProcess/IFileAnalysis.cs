using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IFileAnalysis
    {
        /// <summary>
        /// Transform the data into a new shorter file 
        /// </summary>
        /// <param name="newfileNamePath"></param>
        /// <returns>Rute of the new file</returns>
        Task<string> TransformDataAsync(string newfileNamePath);
        /// <summary>
        /// Process grouped data from a single unified file.
        /// </summary>
        /// <param name="unifiedFileNamePath">Unified file path</param>
        /// <returns>Response model for the console</returns>
        Task<IEnumerable<OutputModel>> ProcessDataAsync(string unifiedFileNamePath);
    }
}