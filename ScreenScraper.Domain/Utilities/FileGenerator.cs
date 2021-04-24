using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Domain.Utilities
{
    public static class FileGenerator
    {
        public static void GenerateFile(string name, string content)
        {
            var fileName = GenerateFileName(name);
            using (StreamWriter outFile = new StreamWriter(fileName, true))
            {
                outFile.WriteLine(content);
                outFile.Flush();
                outFile.Close();
            }
        }

        private static string GenerateFileName(string name)
        {
            return $"{name}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm")}";
        }
    }
}
