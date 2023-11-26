using MediaDevices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private void CleanKindle()
        {           
            string content = string.Empty;
            Dispatcher.Invoke(() =>
            {
                runningNow.Content = Strings.Cleaning;
                progressBar.Value = 0;
                Cursor = Cursors.Wait;
                content = deviceLister.SelectedItem.ToString()!;
            });
            if (content == Strings.KindleOther)
            {
                CleanKindleOther();
            }
            else if (content == Strings.KindleScribe)
            {
                CleanKindleScribe();
            }
            Dispatcher.Invoke(() =>
            {
                Cursor = Cursors.Arrow;
                runningNow.Content = Strings.Cleaned;
            });
            string message = Strings.KindleClean;
            string messageTitle = Strings.KindleCleanTitle;
            MessageBox.Show(message, messageTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void CleanKindleOther()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle") && drive.IsReady)
                {
                    List<string> resultList = new();
                    if (drive.Name.Length > 0)
                    {
                        string configFile = drive.Name + @"metadata.calibre";
                        string thumbnailDir = drive.Name + @"system\thumbnails";
                        string text = File.ReadAllText(configFile);
                        Dispatcher.Invoke(() =>
                        {
                            progressBar.Maximum = Directory.GetFiles(thumbnailDir).Length;
                        });
                        foreach (String file in Directory.GetFiles(thumbnailDir))
                        {
                            string uuid = file.Split("_EBOK_portrait.jpg")[0].Split(@"\thumbnail_")[1];
                            if (!text.Contains(uuid))
                            {
                                File.Delete(file);
                            }
                            Dispatcher.Invoke(() =>
                            {
                                progressBar.Value++;
                            });
                        }                      
                    }
                }
            }
            Dispatcher.Invoke(() =>
            {
                progressBar.Value = progressBar.Maximum;
            });
        }
        private void CleanKindleScribe()
        {
            string whereToCopy = UsefulVariables.GetKindleCoverFixerPath() + @"\metadata_scribe.calibre";
            string text = string.Empty;
            var devicess = MediaDevice.GetDevices();
            using var device = devicess.First(d => d.FriendlyName == "Kindle Scribe");
            device.Connect();
            var objects = device.FunctionalObjects(FunctionalCategory.Storage);
            MediaStorageInfo deviceInfo = device.GetStorageInfo(objects.First());
            Dispatcher.Invoke(() =>
            {
                progressBar.Maximum = device.EnumerateFiles(@"\" + deviceInfo.Description + @"\system\thumbnails").Count();
            });
            using (FileStream stream = File.OpenWrite(whereToCopy))
            {
                device.DownloadFile(@"\" + deviceInfo.Description + @"\metadata.calibre", stream);
            }
            if (File.Exists(whereToCopy))
            {
                text = File.ReadAllText(whereToCopy);
            }
            foreach (string file in device.EnumerateFiles(@"\" + deviceInfo.Description + @"\system\thumbnails"))
            {
                string uuid = file.Split("_EBOK_portrait.jpg")[0].Split(@"\thumbnail_")[1];
                if (!text.Contains(uuid))
                {
                    device.DeleteFile(file);
                }
                Dispatcher.Invoke(() =>
                {
                    progressBar.Value++;
                });
            }
            device.Disconnect();
            List<string> resultList = new();
            Dispatcher.Invoke(() =>
            {
                progressBar.Value = progressBar.Maximum;
            });
        }
    }
}
