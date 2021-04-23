using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface IFileParser
    {
        /// <summary>
        /// Return the rute of the transform data
        /// </summary>
        /// <param name="newfileNamePath"></param>
        /// <returns></returns>
        Task<string> TransformDataAsync(string newfileNamePath);
        Task<IEnumerable<OutputModel>> CountDataAsync(string newfileNamePath);
    }
}