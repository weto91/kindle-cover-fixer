using System.Threading;
using System.Windows;
using System.Diagnostics;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Thread task = new(CoverGenerationTask);
            task.Start();
        }      
        private void FindBooks_Click(object sender, RoutedEventArgs e)
        {
            Thread task = new(FindBooksTask);
            task.Start();
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OutputCreateOrDelete();
            LogLine("INFO", "The application is closing. Bye!");
            LogToFile();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ClearKindle_Click(object sender, RoutedEventArgs e)
        {
            Thread task = new(CleanKindle);
            task.Start();
        }
        private void ClearOutput_Click(object sender, RoutedEventArgs e)
        {
            CleanOrCreateOutput();
            string message = Strings.OutputClean;
            string messageTitle = Strings.OutputCleanTitle;
            MessageBox.Show(message, messageTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/weto91/kindle-cover-fixer/wiki");
        }
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/weto91");
        }
        private void TransferButton_Click(object sender, RoutedEventArgs e)
        {
            Thread task = new(TransferFilesToKindle);
            task.Start();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new();
            settings.ShowDialog();
        }
        private void OpenOutput_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", UsefulVariables.OutputFolder());
        }
        private void CheckLibrary_Click(object sender, RoutedEventArgs e)
        {
            DataGridUserPreparationCalibre();
            CheckCalibreBooks();
        }
        private void CheckKindle_Click(object sender, RoutedEventArgs e)
        {
            DataGridUserPreparationKindle();
            CheckKindleBooks();
        }

        private void LibraryPath_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Thread imageList = new(loadImages);
            imageList.Start();
        }
    }
}
