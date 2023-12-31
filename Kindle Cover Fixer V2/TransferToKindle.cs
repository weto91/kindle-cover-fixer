﻿using MediaDevices;
using System.IO;
using System.Linq;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private void TransferFilesToKindle()
        {
            IsOnKindlePreparation();
            string[] covers = Directory.GetFiles(UsefulVariables.OutputFolder());
            Dispatcher.Invoke(() =>
            {
                generateButton.IsEnabled = false;
                runningNow.Content = Strings.Transferring; 
                progressBar.Maximum = covers.Length;
                progressBar.Value = 0;
            });
            int bookCounter = 1;
            foreach (string cover in covers)
            {
                string[] namea = cover.Split("_EBOK_portrait.jpg");
                string[] uuid = namea[0].Split(@"\thumbnail_");
                string name = "thumbnail_" + uuid[1] + "_EBOK_portrait.jpg";
                FileInfo file = new(cover);
                double size = file.Length * 1048576;
                Dispatcher.Invoke(() =>
                {
                    double fileNumber = progressBar.Value + 1;
                    DataGridTransfer.Items.Add(new DataGridTransferCols { FileNumber = @"[" + fileNumber + @"/" + covers.Length + @"]", FileName = name, FileSize = size.ToString() });
                });
                TransferFilesCheck(cover, name);
                Dispatcher.Invoke(() =>
                {
                    progressBar.Value++;
                });
                bookCounter++;
            }
            Dispatcher.Invoke(() =>
            {
                progressBar.Value++;
                runningNow.Content = Strings.Transferred; 
                LogLine("TRANSFER", "All files was transferred to the device.");
            });
            string message = Strings.KindleTransfer;
            string messageTitle = Strings.KindleTransferTitle;
            MessageBox.Show(message, messageTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void TransferFilesCheck(string fileToCopy, string fileName)
        {
            string content = string.Empty;
            Dispatcher.Invoke(() => 
            {
                content = deviceLister.Text;
            });

            if (content == Strings.KindleOther)
            {
                TransferOther(fileToCopy, fileName);
            }
            else if (content == Strings.KindleScribe)
            {
                TransferScribe(fileToCopy, fileName);
            }
        }
        private void TransferOther(string fileToCopy, string fileName)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle") && drive.IsReady)
                {
                    File.Copy(fileToCopy, drive.Name + @"system\thumbnails\" + fileName, true);
                    if (File.Exists(drive.Name + @"system\thumbnails\" + fileName))
                    {
                        LogLine("SUCCESS", "File copy: " + drive.Name + @"system\thumbnails\" + fileName);
                    }
                    else
                    {
                        LogLine("FAILURE", "File copy: " + drive.Name + @"system\thumbnails\" + fileName);
                    }
                }
            }
        }
        private void TransferScribe(string fileToCopy, string fileName)
        {
            var devices = MediaDevice.GetDevices();
            using var device = devices.First(d => d.FriendlyName == "Kindle Scribe");
            device.Connect();
            var objects = device.FunctionalObjects(FunctionalCategory.Storage);
            MediaStorageInfo deviceInfo = device.GetStorageInfo(objects.First());
            if (device.FileExists(@"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName))
            {
                device.DeleteFile(@"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
                LogLine("SUCCESS", "Delete file (Scribe): " + @"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
            }
            using (FileStream stream = File.OpenRead(fileToCopy))
            {
                device.UploadFile(stream, @"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
                if (device.FileExists(@"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName))
                {
                    LogLine("SUCCESS", "Copy file (Scribe): " + @"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
                }
                else
                {
                    LogLine("SUCCESS", "Copy file (Scribe): " + @"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
                }
            }
            device.Disconnect();
        }
    }
}
