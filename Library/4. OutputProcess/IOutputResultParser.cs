using DS_ProgramingChallengeLibrary.Models;
using System.Collections.Generic;

namespace DS_ProgramingChallengeLibrary
{
    public interface IOutputResultParser
    {
        /// <summary>
        /// Print the analysis result to the console application.
        /// </summary>
        /// <param name="obj">List of models to print in the console application</param>
        void ShowResult(IEnumerable<OutputModel> obj);
    }
}