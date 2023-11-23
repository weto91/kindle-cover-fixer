﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        // Run when the MainWindow size was changed
        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
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