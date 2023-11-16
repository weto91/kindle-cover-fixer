

using System.Diagnostics;
using System.Reflection.Metadata;

namespace Kindle_Cover_Fixer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
            bookList("list");
            button2.Enabled = true;
        }

        private void bookList(string action)
        {
            String[] directoryAuthors = Directory.GetDirectories(textBox1.Text);
            foreach (String author in directoryAuthors)
            {
                if (!author.Contains("caltrash"))
                {
                    String[] directoryBooks = Directory.GetDirectories(author);
                    foreach (String book in directoryBooks)
                    {
                        if (action == "list")
                        {
                            textBox2.Text = textBox2.Text + "\r\n" + book;
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
                            //textBox3.Text = Environment.CurrentDirectory;
                            File.Copy(image, Environment.CurrentDirectory + "\\" + "EXPORTED" + "\\" + "thumbnail_" + uuid + "_EBOK_portrait.jpg");
                        }

                    }
                }

            }
            if (action == "generate")
            {
                string message = "Now. You must move all cover images to: <Kindle Scribe>/System/Thumbnails (replace if its necessary)\r\n \r\nYou can click on Yes to open de exported folder. ";
                string caption = "Covers generation finished";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OpenFolder(Environment.CurrentDirectory + "\\" + "EXPORTED");
                }
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
        private void button2_Click(object sender, EventArgs e)
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

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
            }
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://github.com/weto91");
        }
    }
}