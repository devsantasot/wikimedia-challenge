
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary
{
    public class FileAnalysisBase
    {
        protected readonly ILogger _log;
        protected readonly IFileSystem _fileSystem;

        public FileAnalysisBase(ILogger log, IFileSystem fileSystem)
        {
            _log = log;
            _fileSystem = fileSystem;
        }

        internal static List<DataModelSummary> GetDataModelSumFromFile(string fileNamePath, char[] separator)
        {
            List<DataModelSummary> dataModel = new();
            try
            {
                using (FileStream fs = File.Open(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var columns = line.Split(separator);

                            string domain_code = columns[0];
                            string page_title = columns[1];

                            if (int.TryParse(columns[2], out int count_views) == false)
                            {
                                foreach (var item in columns)
                                {
                                    if (int.TryParse(item, out count_views))
                                        break;
                                }
                            }

                            dataModel.Add(new DataModelSummary()
                            {
                                domain_code = domain_code,
                                page_title = page_title,
                                count_views = count_views
                            });
                        }
                    }
                }
            }
            finally
            {
                GC.Collect();
            }

            return dataModel;
        }

        internal static IEnumerable<DataModelSummary> GroupBySumData(IEnumerable<DataModelSummary> dataModel)
        {
            return (from e in dataModel
                    group e by new { e.domain_code, e.page_title } into gb
                    where gb.Sum(e => e.count_views) > 1
                    select new DataModelSummary
                    {
                        domain_code = gb.Key.domain_code,
                        page_title = gb.Key.page_title,
                        count_views = gb.Sum(e => e.count_views)
                    })
                    ;
        }

        internal static IEnumerable<DataModelSummary> GroupByMaxData(IEnumerable<DataModelSummary> containedData)
        {
            return containedData
               .GroupBy(c => new { c.domain_code })
               .Select(gb => new DataModelSummary()
               {
                   domain_code = gb.Key.domain_code,
                   count_views = gb.Max(x => x.count_views)
               })
               .OrderByDescending(i => i.count_views)
               .Take(100);
        }

        internal static IEnumerable<OutputModel> GetOutputModel(List<DataModelSummary> resultGroupBySum, List<DataModelSummary> resultGroupByMax)
        {
            return from r in resultGroupByMax
                   join g in resultGroupBySum on new { r.domain_code, r.count_views } equals new { g.domain_code, g.count_views }
                   select new OutputModel
                   {
                       domain_code = r.domain_code,
                       page_title = g.page_title,
                       max_count_views = r.count_views
                   };
        }
    }
}