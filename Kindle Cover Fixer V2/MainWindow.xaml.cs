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
using static System.Net.Mime.MediaTypeNames;

namespace Kindle_Cover_Fixer_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {       
        string StateDevice = "DSCON";
        public MainWindow()
        {
            InitializeComponent();
        }
        // Run when generateButton was clicked
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            CoverGenerationTask();
        }      
        // Find the books on selected Calibre library
        private void FindBooks_Click(object sender, RoutedEventArgs e)
        {
            FindBooksTask();
        }
        // Log when the app closing
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogLine("INFO", "The application is closing. Bye!");
        }
    }
}
