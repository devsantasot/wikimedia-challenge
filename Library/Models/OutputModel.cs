using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_ProgramingChallengeLibrary.Models
{
    public class OutputModel
    {
        public string domain_code { get; set; }
        public string page_title { get; set; }
        public int max_count_views { get; set; }
    }

    public class GroupByOutputModel
    {
        public string FileName { get; set; }
        //public List<ContainedDataModel> containedDataModel { get; set; }
    }
}
