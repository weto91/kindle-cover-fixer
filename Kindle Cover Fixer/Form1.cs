

using System.Diagnostics;
using MediaDevices;
using Octokit;

namespace Kindle_Cover_Fixer
{
    public partial class MainScreen : Form
    {
        private string userDir = string.Empty;
        private string fileLibrary = string.Empty;
        private bool isLibrary = false;

        public string UserDir { get => userDir; set => userDir = value; }
        public string OutputDir { get; set; } = string.Empty;
        public string FileLibrary { get => fileLibrary; set => fileLibrary = value; }
        public bool IsLibrary { get => isLibrary; set => isLibrary = value; }

        public MainScreen()
        {
            if (Environment.GetEnvironmentVariable("USERPROFILE") != null)
            {
                #pragma warning disable CS8601
                UserDir = Environment.GetEnvironmentVariable("LOCALAPPDATA");
                #pragma warning restore CS8601 
                OutputDir = UserDir + @"\KindleCoverFixer\OUTPUT";
                FileLibrary = UserDir + @"\KindleCoverFixer\library.kcf";
            }
            else
            {
                OutputDir = Environment.CurrentDirectory + @"\OUTPUT";
                FileLibrary = Environment.CurrentDirectory + @"\library.kcf";
            }
            InitializeComponent();
            versionLabel.Text = "Version 1.6";
            CreateLibraryFile();
            CheckGitHubNewerVersion();
            CheckKindleType();
            CreateOutputDir();
            BookList("list");
        }
        // ACTIONS
        private void GenerateCoversButton_Click(object sender, EventArgs e)
        {
            CreateOutputDir();
            BookList("generate");
        }
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OpenExportedDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(OutputDir))
            {
                OpenFolder(OutputDir);
            }
            else
            {
                string message = "This directory will be created after the process execution.";
                string caption = "Directory does not exists yet";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
        }
        private void ManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpScreen frm = new();
            frm.Show();
            this.Hide();
        }
        private void GitHubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://github.com/weto91");
        }
        private void TransferButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Covers transfered";
            bookListPath.Text = string.Empty;
            TransferFilesToKindle();
            bookListPath.AppendText("\n\r" + string.Empty + "\n\r" + string.Empty + "\n\r" + "All Covers transferred. Enjoy!");
        }
        private void UpdateButton_ButtonClick(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://github.com/weto91/kindle-cover-fixer/releases/tag/working_release");
        }
        private void SelectLibraryButton_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath.Length > 1)
            {
                libraryPath.Text = folderBrowserDialog1.SelectedPath;
                CheckLibrary();
                if (IsLibrary)
                {
                    File.WriteAllText(FileLibrary, folderBrowserDialog1.SelectedPath);
                    BookList("list");
                    generateCoversButton.Enabled = true;
                }
            }
        }
        private void ConnectDevice_ButtonClick_1(object sender, EventArgs e)
        {
            CheckKindleType();
        }
        // FUNCTIONS
        private void CheckLibrary()
        {
            IsLibrary = false;
            if (libraryPath.Text.Length > 5)
            {
                string[] files = Directory.GetFiles(libraryPath.Text);
                foreach (string file in files)
                {
                    if (file.Contains("metadata.db"))
                    {
                        IsLibrary = true;
                    }
                }
                if (!IsLibrary)
                {
                    libraryPath.Text = string.Empty;
                    generateCoversButton.Enabled = false;
                    string message = "Sorry, the selected path is not a Calibre Library. \r\n \r\n Be sure to select the appropriate caliber library. ";
                    string caption = "This is not Calibre library";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                }
            }
        }
        private void CreateOutputDir()
        {

            if (Directory.Exists(OutputDir))
            {
                Directory.Delete(OutputDir, true);
            }
            Directory.CreateDirectory(OutputDir);
        }
        private void CreateLibraryFile()
        {
            if (!File.Exists(FileLibrary))
            {
                FileStream fs = File.Create(FileLibrary);
                fs.Close();
            }
        }
        private void CheckKindleType()
        {

            deviceName.Text = string.Empty;
            var devices = MediaDevice.GetDevices();
            DriveInfo[] drives = DriveInfo.GetDrives();
            bool otherKindle = false;
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle"))
                {
                    otherKindle = true;
                }
            }
            if (devices.Any())
            {
                if (devices.First().FriendlyName == "Kindle Scribe")
                {
                    deviceName.Text = "Kindle Scribe";
                    connectDevice.Visible = false;
                }
                else if (otherKindle)
                {
                    deviceName.Text = "Kindle (Other)";
                    connectDevice.Visible = false;
                }
                else
                {
                    deviceName.Text = "Kindle not connected";
                    connectDevice.Visible = true;
                }
            }
            else
            {
                deviceName.Text = "Kindle not connected";
                connectDevice.Visible = true;
            }
        }
        private async void CheckGitHubNewerVersion()
        {
            GitHubClient client = new(new ProductHeaderValue("Kindle_Cover_Fixer"));
            var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");
            var latest = releases.Name;
            float latestVersion = float.Parse(latest.ToString().Split(' ')[1]);
            float localVersion = float.Parse(versionLabel.Text.Split(' ')[1]);
            if (latestVersion > localVersion)
            {
                versionLabel.Text += " (New version available)";
                updateButton.Visible = true;
            }
            else
            {
                versionLabel.Text += " (Up to date)";
            }
        }
        private void BookList(string action)
        {
            if (IsLibrary)
            {
                string[] directoryAuthors = Directory.GetDirectories(libraryPath.Text);
                foreach (string author in directoryAuthors)
                {
                    if (!author.Contains("caltrash"))
                    {
                        string[] directoryBooks = Directory.GetDirectories(author);
                        foreach (string book in directoryBooks)
                        {
                            if (action == "list")
                            {
                                bookListPath.AppendText(book + "\r\n");
                            }
                            if (action == "generate")
                            {
                                string uuid = string.Empty;
                                string image = string.Empty;
                                string[] fileBooks = Directory.GetFiles(book);
                                foreach (string file in fileBooks)
                                {
                                    if (file.Contains("metadata.opf"))
                                    {
                                        string[] lines = File.ReadAllLines(file);
                                        foreach (string line in lines)
                                        {
                                            if (line.Contains("uuid_id"))
                                            {
                                                //<dc:identifier opf:scheme="uuid" id="uuid_id">f97c1f8a-7ad3-42e2-a928-5f9f5b0a200d</dc:identifier>
                                                string[] uuida = line.Split('>');
                                                string[] uuidb = uuida[1].Split("<");
                                                uuid = uuidb[0];
                                            }
                                        }
                                    }
                                    if (file.Contains("cover.jpg"))
                                    {
                                        image = file;
                                    }
                                }
                               File.Copy(image, OutputDir + @"\" + "thumbnail_" + uuid + "_EBOK_portrait.jpg");
                            }
                        }
                    }
                }
            }
            if (action == "generate")
            {
                string message = "Now. You must move all cover images to: <Kindle>/System/Thumbnails (replace if its necessary)\r\n or you can click on \"Transfer button\" to send files to the Kindle \r\n \r\n Also, you can click on Yes to open de exported folder. ";
                string caption = "Covers generation finished";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OpenFolder(OutputDir);
                }
                transferButton.Visible = true;
                CheckKindleType();
                if (deviceName.Text == "Kindle (Other)" || deviceName.Text == "Kindle Scribe")
                {
                    transferButton.Enabled = true;
                }

            }
        }
        private static void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new()
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
            }
        }
        private static void TransferScribe(string fileToCopy, string fileName)
        {
            var devicess = MediaDevice.GetDevices();
            using var device = devicess.First(d => d.FriendlyName == "Kindle Scribe");
            device.Connect();
            var objects = device.FunctionalObjects(FunctionalCategory.Storage);
            MediaStorageInfo deviceInfo = device.GetStorageInfo(objects.First());
            device.DeleteFile(@"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
            using (FileStream stream = File.OpenRead(fileToCopy))
            {
                device.UploadFile(stream, @"\" + deviceInfo.Description + @"\system\thumbnails\" + fileName);
            }
            device.Disconnect();
        }
        private static void TransferOther(string fileToCopy, string fileName)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.VolumeLabel.Contains("Kindle"))
                {
                    File.Copy(fileToCopy, drive.Name + @"system\thumbnails\" + fileName, true);
                }
            }
        }
        private void TransferFiles(string fileToCopy, string fileName)
        {
            if (deviceName.Text == "Kindle (Other)")
            {
                TransferOther(fileToCopy, fileName);
            }
            else if (deviceName.Text == "Kindle Scribe")
            {
                TransferScribe(fileToCopy, fileName);
            }
        }
        private void TransferFilesToKindle()
        {
            string[] covers = Directory.GetFiles(OutputDir);
            progressBarTransfer.Maximum = covers.Length;
            progressBarTransfer.Value = 0;
            int bookCounter = 1;
            foreach (string cover in covers)
            {
                string[] namea = cover.Split(".jpg");
                string[] nameb = namea[0].Split(@"\thumbnail");
                string name = "thumbnail" + nameb[1] + ".jpg";
                bookListPath.AppendText("[" + bookCounter.ToString() + "/" + covers.Length.ToString() + "] " + name + "\r\n");
                TransferFiles(cover, name);
                progressBarTransfer.Value += progressBarTransfer.Step;
                bookCounter++;
            }
        }
    }
}