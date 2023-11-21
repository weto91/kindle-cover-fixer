using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Kindle_Cover_Fixer_V2
{

    public class UsefulVariables
    {
        // Define the application version
        public const string AppVersion = "2.0";
        // Determine the %APPDATA% folder
        public static string OutputFolder()
        {
            string? appData = Environment.GetEnvironmentVariable("APPDATA");
            string result = appData + @"\Kindle Cover Fixer\Output";
            if(appData != null) 
            {
                return result;
            }
            else
            {
                return Environment.CurrentDirectory + @"\Output";
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
