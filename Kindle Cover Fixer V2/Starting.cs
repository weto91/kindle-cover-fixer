using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow : Window
    {
        // Create Output directory if not exists or delte its content if exists and the log file
        private static void OutputCreateOrDelete()
        {
            // output
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
            // log file
            string text = @"Started on " + System.DateTime.Now.ToString();
            File.WriteAllText(UsefulVariables.LogFile(), text);
            LogLine("INFO", "Started at: " + DateTime.Now);
            LogLine("INFO", "OS version: " + Environment.OSVersion);
            LogLine("INFO", "Process ID: " + Environment.ProcessId);
            LogLine("INFO", "System memory page: " + Environment.SystemPageSize);
            LogLine("INFO", "Version: " + Environment.Version);
            LogLine("INFO", "KCF Path: " + Environment.ProcessPath);
            LogLine("INFO", "KCF Version: " + UsefulVariables.AppVersion);
            LogLine("INFO", "#############################################################");
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
    }
}
