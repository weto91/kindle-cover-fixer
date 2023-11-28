using MediaDevices;
using System;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        bool firstTime = true;
        private void CheckKindle()
        {
            Timer aTimer = new(2000);
            aTimer.Elapsed += OnTimedEvent!;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            CheckKindleType();
        }
        // Check the kindle type that you have connected
        private void CheckKindleType()
        {
            int countDevices = 0;
            string selected = string.Empty;
            Collection<string> list = new();
            Dispatcher.Invoke(() =>
            {
                countDevices = deviceLister.Items.Count;
                if (countDevices > 0)
                {
                    selected = deviceLister.SelectedItem.ToString()!;
                }                
            });
            var devices = MediaDevice.GetDevices();
            DriveInfo[] drives = DriveInfo.GetDrives();
            bool otherKindle = false;
            bool scribeKindle = false;
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    if (drive.VolumeLabel.Contains("Kindle"))
                    {
                        otherKindle = true;
                        if (selected == Strings.KindleOther)
                        {
                            list.Insert(0, Strings.KindleOther);                       
                        }
                        else
                        {
                            list.Add(Strings.KindleOther);
                        }                      
                    }
                }
            }
            if (devices.Any())
            {
                foreach (MediaDevice device in devices)
                {
                    if (device.FriendlyName == "Kindle Scribe")
                    {
                        scribeKindle = true;
                        if (selected == Strings.KindleScribe)
                        {
                            list.Insert(0, Strings.KindleScribe);
                        }
                        else
                        {
                            list.Add(Strings.KindleScribe);
                        }
                    }
                }
            }
            if (!otherKindle && !scribeKindle) 
            {
                Dispatcher.Invoke(() =>
                {
                    list.Add(Strings.KindleNone);
                });
            }
            Dispatcher.Invoke(() =>
            {
                deviceLister.Items.Clear();
                foreach (string device in list)
                {
                    deviceLister.Items.Add(device);
                }
                if (deviceLister.Items.Count != countDevices)
                {
                    string deviceList = string.Empty;
                    foreach (string device in deviceLister.Items)
                    {
                        deviceList += device + " | ";
                    }
                    LogLine("DEVICE", "Device connected or disconnected, new device list: " + deviceList);
                } 
                deviceLister.SelectedIndex = 0;
                if (deviceLister.Text == Strings.KindleOther || deviceLister.Text == Strings.KindleScribe)
                {
                    if (firstTime)
                    {
                        runningNow.Content = Strings.Ready;
                        firstTime = false;
                    }
                    
                    findBooks.IsEnabled = true;
                }
                else
                {
                    findBooks.IsEnabled = false;
                    generateButton.IsEnabled = false;
                    transferButton.IsEnabled = false;
                    runningNow.Content = Strings.ConnectKindle;
                }
            });
        }
    }
}
