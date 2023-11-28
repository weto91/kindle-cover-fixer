using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using VersOne;
using VersOne.Epub;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private string[] RealBookUuid(string path, string calibreUuid)
        {
            string[] files = Directory.GetFiles(path);
            string bookUuid = string.Empty;
            bool wrongFormat = false;
            foreach (string item in files)
            {             
                string regex = "[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}";
                if (item.Contains(".mobi") || item.Contains(".azw3"))
                {
                    string text = File.ReadAllText(item);
                    if (text.Contains(calibreUuid))
                    {
                        Match findBookUuid = Regex.Match(text, "," + regex, RegexOptions.IgnoreCase);
                        if (findBookUuid.Groups[0].Value.Length == 37)
                        {
                            wrongFormat = false;
                            bookUuid = findBookUuid.Groups[0].Value.Split(",")[1];
                            string[] resultTrue = new string[] {bookUuid, "true" };
                            return resultTrue;
                        }
                    }
                    else
                    {
                        Match findBookUuid = Regex.Match(text, "," + regex, RegexOptions.IgnoreCase);
                        if (findBookUuid.Groups[0].Value.Length == 37)
                        {
                            wrongFormat = false;
                            bookUuid = findBookUuid.Groups[0].Value.Split(",")[1];
                            string[] resultFalse = new string[] { bookUuid, "false" };
                            return resultFalse;
                        }
                    }
                }
                else
                {
                    wrongFormat = true;
                }
            }
            if (wrongFormat)
            {
                string[] result = new string[] { "", "WF" };
                return result;
            }
            else
            {
                string[] result = new string[] { "", "false" };
                return result;
            }
        }
    }
}
