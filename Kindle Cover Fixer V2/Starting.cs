using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow : Window
    {
        // Create Output directory if not exists or delte its content if exists and the log file
        private void OutputCreateOrDelete()
        {
            // output
            CleanOrCreateOutput();
            // log file
            string logPath = UsefulVariables.GetKindleCoverFixerPath() + @"\Log";
            if (Directory.Exists(logPath))
            {
                FirstLog();
            }
            else
            {
                Directory.CreateDirectory(logPath);
                FirstLog();
            }    
        }
        // Define the User information DataGrid structure for Find books
        private void DataGridUserPreparation()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
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
            col0.Header = Strings.BookNumber; //DGUNU
            col1.Header = Strings.BookName; //DGUNA
            col2.Header = Strings.BookUuid; //DGUU
            col3.Header = Strings.BookPassed; //DGUP
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the User information DataGrid structure for Generate covers
        private void DataGridUserPreparationGen()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
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
            col0.Header = Strings.BookNumber; 
            col1.Header = Strings.BookName; 
            col2.Header = Strings.BookUuid; 
            col3.Header = Strings.BookStatus; 
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the User information DataGrid structure for checks Calibre
        private void DataGridUserPreparationCalibre()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
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
            col0.Header = Strings.BookNumber; 
            col1.Header = Strings.BookName; 
            col2.Header = Strings.BookUuid; 
            col3.Header = Strings.BookProblem; 
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the User information DataGrid structure for checks Calibre
        private void DataGridUserPreparationKindle()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
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
            col0.Header = Strings.BookNumber;
            col1.Header = Strings.BookName;
            col2.Header = Strings.BookLibrary;
            col3.Header = Strings.BookProblem;
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the System information DataGrid structure
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
            ControlStrings();
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
            runningNow.Content = Strings.Ready; 
        }
        private void ControlStrings()
        {
            libraryPathLabel.Content = Strings.CalibreLib;
            findBooks.Content = Strings.FindBooks;
            generateButton.Content = Strings.GenerateCovers;
            transferButton.Content = Strings.TransferToKindle;
            fileButt.Header = Strings.File;
            settingsButton.Header = Strings.Settings;
            exitButton.Header = Strings.Exit;
            toolsButt.Header = Strings.Tools;
            clearKindle.Header = Strings.ClearKindle;
            clearOutput.Header = Strings.ClearOutput;
            helpButton.Header = Strings.Help;
            aboutButton.Header = Strings.About;
            imageGroupBox.Header = Strings.ImageGroup;
            mainWindow.Width = mainWindow.ActualWidth + 1; // WorkArround to view correctly the FindBook Buttons           
        }
    }
}
