namespace Kindle_Cover_Fixer
{
    partial class MainScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            label1 = new Label();
            libraryPath = new TextBox();
            selectLibraryButton = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            bookListPath = new TextBox();
            groupBox1 = new GroupBox();
            generateCoversButton = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openExportedDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            manualToolStripMenuItem = new ToolStripMenuItem();
            gitHubLinkLabel = new LinkLabel();
            transferButton = new Button();
            statusStrip1 = new StatusStrip();
            progressBarTransfer = new ToolStripProgressBar();
            versionLabel = new ToolStripStatusLabel();
            updateButton = new ToolStripSplitButton();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 33);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 0;
            label1.Text = "Calibre library:";
            // 
            // libraryPath
            // 
            libraryPath.Location = new Point(101, 30);
            libraryPath.Name = "libraryPath";
            libraryPath.Size = new Size(594, 23);
            libraryPath.TabIndex = 1;
            // 
            // selectLibraryButton
            // 
            selectLibraryButton.Location = new Point(701, 30);
            selectLibraryButton.Name = "selectLibraryButton";
            selectLibraryButton.Size = new Size(96, 23);
            selectLibraryButton.TabIndex = 2;
            selectLibraryButton.Text = "Seleccionar";
            selectLibraryButton.UseVisualStyleBackColor = true;
            selectLibraryButton.Click += selectLibraryButton_Click;
            // 
            // bookListPath
            // 
            bookListPath.BackColor = SystemColors.Window;
            bookListPath.Location = new Point(6, 22);
            bookListPath.MaxLength = 999999;
            bookListPath.Multiline = true;
            bookListPath.Name = "bookListPath";
            bookListPath.ReadOnly = true;
            bookListPath.ScrollBars = ScrollBars.Both;
            bookListPath.Size = new Size(773, 383);
            bookListPath.TabIndex = 999;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bookListPath);
            groupBox1.Location = new Point(12, 59);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(785, 411);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Books directory list";
            // 
            // generateCoversButton
            // 
            generateCoversButton.Enabled = false;
            generateCoversButton.Location = new Point(692, 476);
            generateCoversButton.Name = "generateCoversButton";
            generateCoversButton.Size = new Size(105, 23);
            generateCoversButton.TabIndex = 5;
            generateCoversButton.Text = "Generate covers";
            generateCoversButton.UseVisualStyleBackColor = true;
            generateCoversButton.Click += generateCoversButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(813, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openExportedDirectoryToolStripMenuItem, toolStripSeparator1, closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openExportedDirectoryToolStripMenuItem
            // 
            openExportedDirectoryToolStripMenuItem.Name = "openExportedDirectoryToolStripMenuItem";
            openExportedDirectoryToolStripMenuItem.Size = new Size(203, 22);
            openExportedDirectoryToolStripMenuItem.Text = "Open exported directory";
            openExportedDirectoryToolStripMenuItem.Click += openExportedDirectoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(200, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(203, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manualToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // manualToolStripMenuItem
            // 
            manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            manualToolStripMenuItem.Size = new Size(114, 22);
            manualToolStripMenuItem.Text = "Manual";
            manualToolStripMenuItem.Click += manualToolStripMenuItem_Click;
            // 
            // gitHubLinkLabel
            // 
            gitHubLinkLabel.AutoSize = true;
            gitHubLinkLabel.Location = new Point(12, 484);
            gitHubLinkLabel.Name = "gitHubLinkLabel";
            gitHubLinkLabel.Size = new Size(152, 15);
            gitHubLinkLabel.TabIndex = 7;
            gitHubLinkLabel.TabStop = true;
            gitHubLinkLabel.Text = "https://github.com/weto91";
            gitHubLinkLabel.LinkClicked += gitHubLinkLabel_LinkClicked;
            // 
            // transferButton
            // 
            transferButton.Enabled = false;
            transferButton.Location = new Point(522, 476);
            transferButton.Name = "transferButton";
            transferButton.Size = new Size(164, 23);
            transferButton.TabIndex = 8;
            transferButton.Text = "Transfer Covers to Kindle Scribe";
            transferButton.UseVisualStyleBackColor = true;
            transferButton.Visible = false;
            transferButton.Click += transferButton_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { progressBarTransfer, versionLabel, updateButton });
            statusStrip1.Location = new Point(0, 517);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(813, 22);
            statusStrip1.TabIndex = 10;
            statusStrip1.Text = "statusStrip1";
            // 
            // progressBarTransfer
            // 
            progressBarTransfer.Name = "progressBarTransfer";
            progressBarTransfer.Size = new Size(100, 16);
            progressBarTransfer.Step = 1;
            // 
            // versionLabel
            // 
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(0, 17);
            // 
            // updateButton
            // 
            updateButton.BackColor = SystemColors.ButtonFace;
            updateButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            updateButton.DropDownButtonWidth = 0;
            updateButton.ForeColor = Color.Red;
            updateButton.ImageScaling = ToolStripItemImageScaling.None;
            updateButton.ImageTransparentColor = Color.Magenta;
            updateButton.Margin = new Padding(5, 2, 0, 0);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(54, 20);
            updateButton.Text = "UPDATE";
            updateButton.TextImageRelation = TextImageRelation.TextAboveImage;
            updateButton.Visible = false;
            updateButton.ButtonClick += updateButton_ButtonClick;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(813, 539);
            Controls.Add(statusStrip1);
            Controls.Add(transferButton);
            Controls.Add(gitHubLinkLabel);
            Controls.Add(generateCoversButton);
            Controls.Add(groupBox1);
            Controls.Add(selectLibraryButton);
            Controls.Add(libraryPath);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainScreen";
            Text = "Kindle Cover Fixer";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox libraryPath;
        private Button selectLibraryButton;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox bookListPath;
        private GroupBox groupBox1;
        private Button generateCoversButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openExportedDirectoryToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem manualToolStripMenuItem;
        private LinkLabel gitHubLinkLabel;
        private Button transferButton;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar progressBarTransfer;
        private ToolStripStatusLabel versionLabel;
        private ToolStripSplitButton updateButton;
    }
}