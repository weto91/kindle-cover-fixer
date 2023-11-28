using MediaDevices;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private bool IsOnKindle(string uuid, int firstTime)
        {
            bool canDoIt = false;
            string metadataFile = UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.kcf";
            if (File.Exists(metadataFile))
            {
                string text = File.ReadAllText(metadataFile);
                if (text.Contains(uuid))
                {
                    canDoIt = true;
                }              
            }
            return canDoIt;
        }
        private void IsOnKindlePreparation()
        {
            if (File.Exists(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.calibre"))
            {
                File.Delete(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.calibre");
            }
            string content = string.Empty;
            Dispatcher.Invoke(() =>
            {
                    content = deviceLister.Text;             
            });
            if (content == Strings.KindleOther)
            {
                IsOnKindleOther();
            }
            else if (content == Strings.KindleScribe)
            {
                IsOnKindleScribe();
            }
        }
        private static void IsOnKindleOther()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle") && drive.IsReady)
                {
                    File.Copy(drive.Name + @"metadata.calibre" , UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.calibre", true);
                    prepareFile();
                }
            }
        }
        private static void IsOnKindleScribe()
        {
            string whereToCopy = UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.calibre";
            var devices = MediaDevice.GetDevices();
            foreach (MediaDevice device in devices)
            {
                if (device.FriendlyName == "Kindle Scribe")
                {
                    device.Connect();
                    var objects = device.FunctionalObjects(FunctionalCategory.Storage);
                    MediaStorageInfo deviceInfo = device.GetStorageInfo(objects.First());
                    using (FileStream stream = File.OpenWrite(whereToCopy))
                    {
                        device.DownloadFile(@"\" + deviceInfo.Description + @"\metadata.calibre", stream);
                    }
                    device.Disconnect();
                    prepareFile();
                }
            }
        }
        private static void prepareFile()
        {
            string text = File.ReadAllText(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.calibre");
            JArray libJson = JArray.Parse(text);
            string result = string.Empty;
            foreach (JObject obj in libJson)
            {
                JToken token = obj["uuid"]!;
                result += token.ToString() + "\r\n";
            }
            File.WriteAllText(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.kcf", result);          
        }
    }
}
