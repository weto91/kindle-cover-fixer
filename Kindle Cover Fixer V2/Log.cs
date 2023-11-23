using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        // Log file new line generator
        private static void LogLine(string logType, string logDescription)
        {
            string text = File.ReadAllText(UsefulVariables.LogFile()) + "[" + logType + "] " + DateTime.Now.ToString() + " | " + logDescription + "\r\n";
            File.WriteAllText(UsefulVariables.LogFile(), text);
        }
    }
}
