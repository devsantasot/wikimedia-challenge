using DS_ProgramingChallengeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DS_ProgramingChallengeLibrary.Helpers;

namespace DS_ProgramingChallengeLibrary
{
    public abstract class FileSystemBase : IFileSystem
    {
        protected readonly ILogger _log;
        protected readonly IConfiguration _config;

        public FileSystemBase(ILogger<FileSystemBase> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public abstract Task SaveDataAsync(IEnumerable<ContainedDataModel> resultGroupBy, string fileNamePath);
        
    }
}