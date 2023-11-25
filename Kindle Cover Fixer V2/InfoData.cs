using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Kindle_Cover_Fixer_V2
{
    public class UsefulVariables
    {
        // Read or Write settings for the application
        public static bool[] Settings()
        {
            List<bool> settingList = new();
            if (File.Exists(UsefulVariables.GetKindleCoverFixerPath() + @"\settings.conf"))
            {
                foreach (string line in File.ReadLines(UsefulVariables.GetKindleCoverFixerPath() + @"\settings.conf"))
                {
                    settingList.Add(bool.Parse(line.Split(": ")[1]));
                }
            }
            else
            {
                settingList.Add(true);
                settingList.Add(true);
            }
            bool[] settingsArr = settingList.ToArray();
            return settingsArr;
        }
        // Define the application version
        public const string AppVersion = "2.4";
        // Determine the output folder path
        public static string OutputFolder()
        {
            string output = GetKindleCoverFixerPath() + @"\Output";
            return output;
        }
        // Determine the log file path
        public static string LogFile()
        {
            string output = GetKindleCoverFixerPath() + @"\Log\Kindle_Cover_Fixer.log";
            return output;
        }
        // Determine the Kindle Cover Fixer folder path
        public static string GetKindleCoverFixerPath()
        {
            string? appData = Environment.GetEnvironmentVariable("APPDATA");
            string result = appData + @"\Kindle Cover Fixer";
            if (appData != null)
            {
                return result;
            }
            else
            {
                return Environment.CurrentDirectory;
            }
        }
        // Determine where is the Calibre configuration file
        public static string[] CalibreLibraries()
        {
            List<string> resultList = new();
            string? path = Environment.GetEnvironmentVariable("APPDATA");
            string configFile;
            if (path?.Length > 0)
            {  
                configFile = path + @"\calibre\gui.json";
                string text = File.ReadAllText(configFile);
                JObject? libJson = JObject.Parse(text);
                JToken? libJsonRes = libJson["library_usage_stats"];
                if (libJsonRes is not null)
                {
                    string[] lines = libJsonRes.ToString().Split(Environment.NewLine.ToCharArray());
                    foreach (string line in lines)
                    {
                        if (line != string.Empty && line != "}" && line != "{")
                        {
                            resultList.Add(line.Split(": ")[0].Split('"')[1]);
                        }                       
                    }
                }
                else
                {
                    resultList.Add("[ERROR] Cant find Calibre Libraries in its configuration files.");
                }       
            }
            else
            {
                resultList.Add("[ERROR] Cant find where is the APPDATA folder in the system.");
            }
            string[] result = resultList.ToArray();
            return result;
        }
    }
}
