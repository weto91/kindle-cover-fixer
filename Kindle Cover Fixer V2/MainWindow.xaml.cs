using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Threading;
using Octokit;
using MediaDevices;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Timers;

namespace Kindle_Cover_Fixer_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //
        // THREADS
        //

        // Thread to resize columns in DataGridUser giving 1ms.
        private void ResizeThread()
        {
            Thread.Sleep(10);
            Dispatcher.Invoke(() => { PathColumnDimension(); });
        }
        private void CheckKindle()
        {
            System.Timers.Timer aTimer = new(2000);
            aTimer.Elapsed += OnTimedEvent!;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        //
        // USEFUL FUNCTIONS
        //

        // Check device and start the transfer Task
        private void TransferFilesCheck(string fileToCopy, string fileName)
        {
            string content = string.Empty;
            Dispatcher.Invoke(() => { content = connectedDevice.Content.ToString(); });
            if (content == "Kindle (Other)" )
            {
                TransferOther(fileToCopy, fileName);
            }
            else if (content == "Kindle Scribe")
            {
                TransferScribe(fileToCopy, fileName);
            }
        }
        // Transfer to Scribe Task
        private static void TransferOther(string fileToCopy, string fileName)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle") && drive.IsReady)
                {
                    File.Copy(fileToCopy, drive.Name + @"system\thumbnails\" + fileName, true);
                }
            }
        }
        // Transfer to other Task
        private static void TransferScribe(string fileToCopy, string fileName)
        {
            var devicess = MediaDevice.GetDevices();
            using var device = devicess.First(d => d.FriendlyName == "Kindle Scribe");
            device.Connect();
            var objects = device.FunctionalObjects(FunctionalCategory.Storage);
            MediaStorageInfo deviceInfo = device.GetStorageInfo(objects.First());
            if (device.FileExists(@"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName))
            {
                device.DeleteFile(@"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
            }     
            using (FileStream stream = File.OpenRead(fileToCopy))
            {
                device.UploadFile(stream, @"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
            }
            device.Disconnect();
        }
        // Prepare the transfer task and send it to final tasks
        private void TransferFilesToKindle()
        {
            string[] covers = Directory.GetFiles(UsefulVariables.OutputFolder());
            Dispatcher.Invoke(() =>
            {
                runningNow.Content = "Transfering covers...";
                progressBar.Maximum = covers.Length;
                progressBar.Value = 0;
            });
            foreach (string cover in covers)
            {
                string[] namea = cover.Split(".jpg");
                string[] nameb = namea[0].Split(@"\thumbnail");
                string name = "thumbnail" + nameb[1] + ".jpg";
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
            }
            Dispatcher.Invoke(() =>
            {
                progressBar.Value++;
                runningNow.Content = "Covers transferred";
            });
        }
        // Check the kindle type that you have connected
        private void CheckKindleType()
        {
            var devices = MediaDevice.GetDevices();
            DriveInfo[] drives = DriveInfo.GetDrives();
            bool otherKindle = false;
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    if (drive.VolumeLabel.Contains("Kindle"))
                    {
                        otherKindle = true;
                    }
                }
            }
            if (devices.Any())
            {
                if (devices.First().FriendlyName == "Kindle Scribe")
                {
                    Dispatcher.Invoke(() => { 
                        connectedDevice.Content = "Kindle Scribe"; 
                    });
                }
                else if (otherKindle)
                {
                    Dispatcher.Invoke(() => {
                        connectedDevice.Content = "Kindle (Other)";
                    });
                }
                else
                {
                    Dispatcher.Invoke(() => {
                        connectedDevice.Content = "Device not connected";
                    });
                }
            }
            else
            {
                Dispatcher.Invoke(() => {
                    connectedDevice.Content = "Device not connected";
                });
            }
        }
        // Check the version in GitHub and compare it with the application version
        private async void CheckGitHubNewerVersion()
        {
            GitHubClient client = new(new Octokit.ProductHeaderValue("Kindle_Cover_Fixer"));
            var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");
            var latest = releases.Name;
            var features = releases.Body;
            float latestVersion = float.Parse(latest.ToString().Split(' ')[1]);
            float localVersion = float.Parse(UsefulVariables.AppVersion);
            if (latestVersion > localVersion)
            {
                versionApp.Content = "Version " + UsefulVariables.AppVersion + " (New version available)";
                MessageBoxResult result = MessageBox.Show("Do you want to download and install the new version? \r\n \r\n You will get the following features: \r\n \r\n." + features, "New version available!.", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start("explorer", "https://github.com/weto91/kindle-cover-fixer/releases/latest");
                    this.Close();
                }
            }
            else
            {
                versionApp.Content = "Version " + UsefulVariables.AppVersion + " (Up to date)";
            }
        }
        // Create Output directory if not exists or delte its content if exists
        private static void OutputCreateOrDelete()
        {
            if (Directory.Exists(UsefulVariables.OutputFolder()))
            {
                // Delete content
                foreach (string file in Directory.GetFiles(UsefulVariables.OutputFolder()))
                {
                    File.Delete(file);
                }
            }
            else
            {
                // Create directory
                Directory.CreateDirectory(UsefulVariables.OutputFolder());
            }
        }
        // Define the User information DataGrid structure
        private void DataGridUserPreparation()
        {
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            DataGridTextColumn col3 = new();          
            // Columns creation
            DataGridUser.Columns.Add(col0);
            DataGridUser.Columns.Add(col1);
            DataGridUser.Columns.Add(col2);
            DataGridUser.Columns.Add(col3);
            // Column header configuration
            col0.Header = "Book nº";
            col1.Header = "Book Name";
            col2.Header = "UUID";
            col3.Header = "Passed";
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");          
        }
        // Define the User information DataGrid structure
        private void DataGridSystemPreparation()
        {
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            // Columns creation
            DataGridSystem.Columns.Add(col0);
            DataGridSystem.Columns.Add(col1);
            DataGridSystem.Columns.Add(col2);
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FilePath");
            col2.Binding = new Binding("FileUuid");
        }
        // Define the transfer information DataGrid structure
        private void DataGridTransferPreparation()
        {
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            // Columns creation
            DataGridTransfer.Columns.Add(col0);
            DataGridTransfer.Columns.Add(col1);
            DataGridTransfer.Columns.Add(col2);
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileSize");
        }
        // Path column dimension setter
        private void PathColumnDimension()
        {
            if (DataGridUser.Columns.Count == 4)
           {
                    DataGridUser.Columns[1].Width = DataGridUser.ActualWidth - (DataGridUser.Columns[0].ActualWidth + DataGridUser.Columns[2].ActualWidth + DataGridUser.Columns[3].ActualWidth + 25);
           }
        }

        //
        // MAIN WINDOW EVENTS
        //

        // Run when the MainWindow was loaded
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            CheckKindle();
            DisableControl(generateButton);
            DataGridUserPreparation();
            DataGridSystemPreparation();
            DataGridTransferPreparation();
            CheckGitHubNewerVersion();
            OutputCreateOrDelete();
            ListAllLibraries();          
        }
        // Run When the MainWindow was rendered
        private void MainWindowRendered(object sender, EventArgs e)
        {
            PathColumnDimension();
            runningNow.Content = "Ready";
        }
        // Run when the MainWindow size was changed
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double libraryPathDim = e.NewSize.Width - (40 + libraryPathLabel.ActualWidth + findBooks.ActualWidth);
            libraryPath.Width = libraryPathDim;
            statusStripGrid.Width = e.NewSize.Width - 30;
            PathColumnDimension();
        }
        // Add to ComboBox the available libraries on your computer
        private void ListAllLibraries()
        {
            libraryPath.Items.Clear();
            foreach (string library in UsefulVariables.CalibreLibraries())
            {
                if (!library.Contains("[ERROR]"))
                {
                    libraryPath.Items.Add(library);
                }
                else
                {
                    DisableControl(findBooks);
                    DisableControl(generateButton);
                    MessageBox.Show("Please, check that you have a library correctly configured in Calibre.", "Cannot find Calibre library(s).", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
            libraryPath.SelectedIndex = 0;
        }
        // Disable controls
        private static void DisableControl(Button button)
        {
            button.IsEnabled = false;
        }
        // Enable controls
        private static void EnableControl(Button button)
        {
            button.IsEnabled = true;
        }

        //
        // CONTROL EVENTS
        //

        // Run when generateButton was clicked
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            runningNow.Content = "Generating new covers...";
            progressBar.Maximum = DataGridSystem.Items.Count;
            progressBar.Value = 0;
            DataGridUser.Items.Clear();
            foreach (DataGridSystemCols dr in DataGridSystem.Items)
            {
                double bookNumber = progressBar.Value + 1;
                string inputPath = dr.FilePath + @"\cover.jpg";
                if (!string.IsNullOrEmpty(inputPath) && !string.IsNullOrEmpty(dr.FilePath) && !string.IsNullOrEmpty(dr.FileUuid))
                {               
                    String outputPath = UsefulVariables.OutputFolder() + @"\thumbnail_" + dr.FileUuid + @"_EBOK_portrait.jpg";
                    File.Copy(inputPath, outputPath, true);
                    DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookNumber + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Cover generated" });
                }
                else
                {
                    DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookNumber + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Cover generated" });
                }
                progressBar.Value++;
            }
            Thread resize = new(ResizeThread);
            resize.Start();
            progressBar.Value++;
            runningNow.Content = "All covers generated";
            TransferFiles transferFiles = new TransferFiles();
            bool? res = transferFiles.ShowDialog();
            if (res == true)
            {
                Thread task = new(TransferFilesToKindle);
                task.Start();
            }         
        }
        // Find the books on selected Calibre library
        private void FindBooks_Click(object sender, RoutedEventArgs e)
        {
            runningNow.Content = "Finding books in your library...";
            //string CalibreLibrary = libraryPath.Text;
            DataGridUser.Items.Clear();
            DataGridSystem.Items.Clear();
            string cs = @"URI=file:" + libraryPath.Text + @"\metadata.db";
            using SQLiteConnection connection = new(cs);
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT * FROM books";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                int bookCounter = 1;
                while (myReader.Read())
                {
                    string canDoIt = "false";
                    string bookPath = libraryPath.Text + @"\" + myReader["path"].ToString();
                    string? bookUuid = myReader["uuid"].ToString();
                    string? bookTitle = myReader["title"].ToString();
                    if (myReader["uuid"].ToString()!.Length >= 10 && myReader["has_cover"].ToString() == "True")
                    {
                        canDoIt = "true";
                        DataGridSystem.Items.Add(new DataGridSystemCols { FileNumber = bookCounter, FileName = bookTitle!, FilePath = bookPath, FileUuid = bookUuid! });
                    }
                    DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookCounter.ToString(), FileName = bookTitle!, FileUuid = bookUuid!, FileCan = canDoIt });
                    bookCounter++;
                }
            }
            connection.Close();
            Thread resize = new(ResizeThread);
            resize.Start();
            runningNow.Content = "All book listed";
            EnableControl(generateButton);
        }
        private void ConnectDevice_Click(object sender, RoutedEventArgs e)
        {
            CheckKindleType();
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            CheckKindleType();
        }

        //
        // OTHER
        //

    }
}
