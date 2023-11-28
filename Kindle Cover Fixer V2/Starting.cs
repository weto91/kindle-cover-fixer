using System;
using System.IO;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow : Window
    {
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
            // Some other data
            if (File.Exists(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.db") && !UsefulVariables.FileIsLocked(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.db"))
            {
                File.Delete(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.db");
            }
            if (File.Exists(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.kcf") && !UsefulVariables.FileIsLocked(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.db"))
            {
                File.Delete(UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.kcf");
            }
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
            runningNow.Content = Strings.ConnectKindle;
            mainWindow.Width = mainWindow.ActualWidth + 1; // WorkArround to view correctly the FindBook Buttons           
        }
    }
}
