using System;
using System.IO;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        string logNewLine = string.Empty;
        // Log file new line generator
        private void LogLine(string logType, string logDescription)
        {
            logNewLine += File.ReadAllText(UsefulVariables.LogFile()) + "[" + logType + "] " + DateTime.Now.ToString() + " | " + logDescription + "\r\n";               
        }
        // Log file to file
        private void LogToFile()
        {
            File.WriteAllText(UsefulVariables.LogFile(), logNewLine);
        }
        private void FirstLog()
        {
            File.WriteAllText(UsefulVariables.LogFile(), "");
            LogLine("INFO", "Started at: " + DateTime.Now);
            LogLine("INFO", "OS version: " + Environment.OSVersion);
            LogLine("INFO", "Process ID: " + Environment.ProcessId);
            LogLine("INFO", "System memory page: " + Environment.SystemPageSize);
            LogLine("INFO", "Version: " + Environment.Version);
            LogLine("INFO", "KCF Path: " + Environment.ProcessPath);
            LogLine("INFO", "KCF Version: " + UsefulVariables.AppVersion);
            LogLine("INFO", "#############################################################");
        }
    }
}
