using MediaDevices;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private bool IsOnKindle(string uuid, int firstTime)
        {
            string content = string.Empty;
            bool result = false;
            Dispatcher.Invoke(() =>
            {
                content = connectedDevice.Content.ToString()!;
            });
            if (content == Strings.KindleOther)
            {
                result = IsOnKindleOther(uuid);
            }
            else if (content == Strings.KindleScribe)
            {
                result = IsOnKindleScribe(uuid, firstTime);
            }
            return result;
        }
        private static bool IsOnKindleOther(string uuid)
        {
            bool canDoIt = false;
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle") && drive.IsReady)
                {
                    List<string> resultList = new();
                    string? path = drive.Name;
                    string configFile;
                    if (path?.Length > 0)
                    {
                        configFile = path + @"metadata.calibre";
                        string text = File.ReadAllText(configFile);
                        if (text.Contains(uuid))
                        {
                            canDoIt = true;
                        }
                    }
                }
            }
            return canDoIt;
        }
        private static bool IsOnKindleScribe(string uuid, int firstTime)
        {
            string whereToCopy = UsefulVariables.GetKindleCoverFixerPath() + @"\metadata_scribe.calibre";
            bool canDoIt = false;
            if (firstTime == 1)
            {              
                var devicess = MediaDevice.GetDevices();
                using var device = devicess.First(d => d.FriendlyName == "Kindle Scribe");
                device.Connect();
                var objects = device.FunctionalObjects(FunctionalCategory.Storage);
                MediaStorageInfo deviceInfo = device.GetStorageInfo(objects.First());
                using (FileStream stream = File.OpenWrite(whereToCopy))
                {
                    device.DownloadFile(@"\" + deviceInfo.Description + @"\metadata.calibre", stream);
                }
                device.Disconnect();
            }      
            List<string> resultList = new();
            if (File.Exists(whereToCopy))
            {
                string text = File.ReadAllText(whereToCopy);
                if (text.Contains(uuid))
                {
                    canDoIt = true;
                }
            }
            return canDoIt;
        }
    }
}
