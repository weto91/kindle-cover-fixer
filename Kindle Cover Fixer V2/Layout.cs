using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        // Run when the MainWindow size was changed
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Trace.WriteLine(e);
            double libraryPathDim = e.NewSize.Width - (40 + libraryPathLabel.ActualWidth + findBooks.ActualWidth);
            libraryPath.Width = libraryPathDim;
            statusStripGrid.Width = e.NewSize.Width - 30;
            PathColumnDimension();
        }
        // Thread to resize columns in DataGridUser giving 1ms.
        private void ResizeThread()
        {
            Thread.Sleep(10);
            Dispatcher.Invoke(() => { PathColumnDimension(); });
        }
        // Path column dimension setter
        private void PathColumnDimension()
        {
            if (DataGridUser.Columns.Count == 4)
            {
                DataGridUser.Columns[1].Width = DataGridUser.ActualWidth - (DataGridUser.Columns[0].ActualWidth + DataGridUser.Columns[2].ActualWidth + DataGridUser.Columns[3].ActualWidth + 25);
            }
            else if (DataGridUser.Columns.Count == 5)
            {
                DataGridUser.Columns[1].Width = DataGridUser.ActualWidth - (DataGridUser.Columns[0].ActualWidth + DataGridUser.Columns[2].ActualWidth + DataGridUser.Columns[3].ActualWidth + DataGridUser.Columns[4].ActualWidth + 20);
            }
        }
        // Disable controls
        private static void DisableControl(Button button)
        {
            button.IsEnabled = false;
        }
        // Enable controls
        private static void EnableControl(Button button)
        {
            button.IsEnabled = true;
        }
    }   
}
