using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private string RealBookUuid(string path)
        {
            string[] files = Directory.GetFiles(path);
            string bookUuid = string.Empty;
            foreach (string item in files)
            {
                if (item.Contains(".mobi"))
                {
                    
                    string text = File.ReadAllText(item);
                    Match findBookUuid = Regex.Match(text, ",[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}", RegexOptions.IgnoreCase);
                    if (findBookUuid.Groups[0].Value.Length == 37)
                    {
                        bookUuid = findBookUuid.Groups[0].Value.Split(",")[1];
                    }
                }
            }
            return bookUuid;
        }
    }
}
