
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS_ProgramingChallengeLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace DS_ProgramingChallengeLibrary
{
    public class OutputResultParser : IOutputResultParser
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;
        static int tableWidth = 73;

        public OutputResultParser(ILogger<OutputResultParser> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void ShowResult(IEnumerable<OutputModel> obj)
        {
            _log.LogInformation("Showing result");

            try
            {
                PrintLine();
                PrintRow("domain_code", "page_title", "max_ count_views");
                PrintLine();
                foreach (OutputModel item in obj)
                {
                    PrintRow(item.domain_code, item.page_title, item.max_count_views.ToString());
                }
                PrintLine();
            }
            finally
            {
                GC.Collect();
            }
            Console.ReadLine();
        }

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
