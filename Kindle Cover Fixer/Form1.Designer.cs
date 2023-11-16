namespace Kindle_Cover_Fixer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            textBox2 = new TextBox();
            groupBox1 = new GroupBox();
            button2 = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openExportedDirectoryToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            manualToolStripMenuItem = new ToolStripMenuItem();
            linkLabel1 = new LinkLabel();
            groupBox1.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            // textBox1
            // 
            textBox1.Location = new Point(101, 30);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(594, 23);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(701, 30);
            button1.Name = "button1";
            button1.Size = new Size(96, 23);
            button1.TabIndex = 2;
            button1.Text = "Seleccionar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Window;
            textBox2.Location = new Point(6, 22);
            textBox2.MaxLength = 999999;
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.Size = new Size(773, 383);
            textBox2.TabIndex = 999;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Location = new Point(12, 59);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(785, 411);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Books directory list";
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(692, 476);
            button2.Name = "button2";
            button2.Size = new Size(105, 23);
            button2.TabIndex = 5;
            button2.Text = "Generate covers";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(806, 24);
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
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(18, 480);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(152, 15);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/weto91";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(806, 508);
            Controls.Add(linkLabel1);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Kindle Cover Fixer";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox textBox2;
        private GroupBox groupBox1;
        private Button button2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openExportedDirectoryToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem manualToolStripMenuItem;
        private LinkLabel linkLabel1;
    }
}