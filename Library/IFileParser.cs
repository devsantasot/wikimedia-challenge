﻿using System.Data;

namespace DS_ProgramingChallengeLibrary
{
    public interface IFileParser
    {
        void TransformDataIntoDataTable(out DataTable resultDataTable);
    }
}