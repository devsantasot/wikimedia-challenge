using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public interface ITransformData
    {

        /// <summary>
        /// Transform the data into a new shorter file 
        /// </summary>
        /// <param name="newfileNamePath"></param>
        /// <returns>Rute of the new file</returns>
        Task<string> TransformDataAsync(string newfileNamePath);
    }
}
