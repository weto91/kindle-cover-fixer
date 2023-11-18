

using System.Diagnostics;
using MediaDevices;
using Octokit;

namespace Kindle_Cover_Fixer
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            versionLabel.Text = "Version 1.2";
            CheckGitHubNewerVersion();

        }
        // ACTIONS
        private void selectLibraryButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath.Length > 1)
            {
                libraryPath.Text = folderBrowserDialog1.SelectedPath;
                bookList("list");
                generateCoversButton.Enabled = true;
            }
        }
        private void generateCoversButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.CurrentDirectory + "\\" + "EXPORTED"))
            {
                Directory.Delete(Environment.CurrentDirectory + "\\" + "EXPORTED", true);
            }
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + "EXPORTED");
            bookList("generate");
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void openExportedDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String directory = Environment.CurrentDirectory + "\\" + "EXPORTED";
            if (Directory.Exists(directory))
            {
                OpenFolder(Environment.CurrentDirectory + "\\" + "EXPORTED");
            }
            else
            {
                string message = "This directory will be created after the process execution.";
                string caption = "Directory does not exists yet";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
            }
        }
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpScreen frm = new HelpScreen();
            frm.Show();
            this.Hide();
        }
        private void gitHubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://github.com/weto91");
        }
        private void transferButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Covers transfered";
            bookListPath.Text = String.Empty;
            transferFilesToKindle();
            bookListPath.AppendText("\n\r" + String.Empty + "\n\r" + String.Empty + "\n\r" + "All Covers transferred. Enjoy!");
        }
        private void updateButton_ButtonClick(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://github.com/weto91/kindle-cover-fixer/releases/tag/working_release");
        }

        // FUNCTIONS
        private async void CheckGitHubNewerVersion()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Kindle_Cover_Fixer"));
            var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");
            var latest = releases.Name;
            String local = versionLabel.Text;
            float latestVersion = float.Parse(latest.ToString().Split(' ')[1]);
            float localVersion = float.Parse(versionLabel.Text.Split(' ')[1]);
            if (latestVersion > localVersion)
            {
                versionLabel.Text = versionLabel.Text + " (New version available)";
                updateButton.Visible = true;
            }
            else
            {
                versionLabel.Text = versionLabel.Text + " (Up to date)";
            }
        }
        private void bookList(string action)
        {
            String[] directoryAuthors = Directory.GetDirectories(libraryPath.Text);
            foreach (String author in directoryAuthors)
            {
                if (!author.Contains("caltrash"))
                {
                    String[] directoryBooks = Directory.GetDirectories(author);
                    foreach (String book in directoryBooks)
                    {
                        if (action == "list")
                        {
                            bookListPath.AppendText(book + "\r\n");
                        }
                        if (action == "generate")
                        {
                            String uuid = String.Empty;
                            String image = String.Empty;
                            String[] fileBooks = Directory.GetFiles(book);
                            foreach (String file in fileBooks)
                            {
                                if (file.Contains("metadata.opf"))
                                {
                                    string[] lines = System.IO.File.ReadAllLines(file);
                                    foreach (string line in lines)
                                    {
                                        if (line.Contains("uuid_id"))
                                        {
                                            //<dc:identifier opf:scheme="uuid" id="uuid_id">f97c1f8a-7ad3-42e2-a928-5f9f5b0a200d</dc:identifier>
                                            String[] uuida = line.Split('>');
                                            String[] uuidb = uuida[1].Split("<");
                                            uuid = uuidb[0];
                                        }
                                    }
                                }
                                if (file.Contains("cover.jpg"))
                                {
                                    image = file;
                                }
                            }
                            File.Copy(image, Environment.CurrentDirectory + "\\" + "EXPORTED" + "\\" + "thumbnail_" + uuid + "_EBOK_portrait.jpg");
                        }
                    }
                }
            }
            if (action == "generate")
            {
                string message = "Now. You must move all cover images to: <Kindle Scribe>/System/Thumbnails (replace if its necessary)\r\n or you can click on \"Transfer button\" to send files to the Kindle \r\n \r\n Also, you can click on Yes to open de exported folder. ";
                string caption = "Covers generation finished";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OpenFolder(Environment.CurrentDirectory + "\\" + "EXPORTED");
                }
                transferButton.Visible = true;
                transferButton.Enabled = true;
            }
        }
        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
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
        private void transferFiles(string fileToCopy, string fileName)
        {
            var devicess = MediaDevice.GetDevices();
            using (var device = devicess.First(d => d.FriendlyName == "Kindle Scribe"))
            {
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
        }
        private void transferFilesToKindle()
        {
            String[] covers = Directory.GetFiles(Environment.CurrentDirectory + @"\EXPORTED");
            progressBarTransfer.Maximum = covers.Length;
            progressBarTransfer.Value = 0;
            int bookCounter = 1;
            foreach (String cover in covers)
            {
                String[] namea = cover.Split(".jpg");
                String[] nameb = namea[0].Split(@"\thumbnail");
                string name = "thumbnail" + nameb[1] + ".jpg";
                bookListPath.AppendText("[" + bookCounter.ToString() + "/" + covers.Length.ToString() + "] " + name + "\r\n");
                transferFiles(cover, name);
                progressBarTransfer.Value = progressBarTransfer.Value + progressBarTransfer.Step;
                bookCounter++;
            }
        }
    }
}