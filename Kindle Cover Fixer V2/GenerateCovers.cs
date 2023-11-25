using System.Threading;
using System.IO;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    // This file have the functions to generate the cover files.
    public partial class MainWindow
    {
        private void CoverGenerationTask()
        {
            System.Windows.Controls.ItemCollection? dgSystemItems = null;
            Dispatcher.Invoke(() =>
            {
                DataGridUserPreparationGen();
                resultLabel.Content = string.Empty;
                runningNow.Content = Strings.Generating;
                progressBar.Maximum = DataGridSystem.Items.Count;
                progressBar.Value = 0;
                DataGridUser.Items.Clear();
                dgSystemItems = DataGridSystem.Items;
            });         
            int bookFailure = 0;
            int bookSuccess = 0;
            int bookTransferible = 0;
            foreach (DataGridSystemCols dr in dgSystemItems!)
            {           
                string inputPath = dr.FilePath + @"\cover.jpg";
                if (!string.IsNullOrEmpty(inputPath) && !string.IsNullOrEmpty(dr.FilePath) && !string.IsNullOrEmpty(dr.FileUuid))
                {
                    string outputPath = UsefulVariables.OutputFolder() + @"\thumbnail_" + dr.FileUuid + @"_EBOK_portrait.jpg";
                    File.Copy(inputPath, outputPath, true);
                    if (File.Exists(outputPath))
                    {
                        File.Delete(outputPath);
                    }
                    ImageResizer(inputPath, outputPath, UsefulVariables.Settings()[0], UsefulVariables.Settings()[1]);
                    if (dr.FileCan == Strings.Yes)
                    {
                        bookTransferible++;
                    }
                    if (File.Exists(outputPath))
                    {
                        bookSuccess++;
                        Dispatcher.Invoke(() =>
                        {
                            double bookNumber = progressBar.Value + 1;
                            DataGridUser.Items.Add(new DataGridUserCols { FileNumber = int.Parse(bookNumber.ToString()) + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Cover generated" });
                        });
                        LogLine("SUCCESS", dr.FileName + " Cover generated");
                    }
                    else
                    {
                        bookFailure++;
                        Dispatcher.Invoke(() =>
                        {
                            double bookNumber = progressBar.Value + 1;
                            DataGridUser.Items.Add(new DataGridUserCols { FileNumber = int.Parse(bookNumber.ToString()) + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Failure" });                         
                        });
                        LogLine("FAILURE", dr.FileName + " Cover generation failed");
                    }
                }
                else
                {
                    bookFailure++;
                    Dispatcher.Invoke(() =>
                    {
                        double bookNumber = progressBar.Value + 1;
                        DataGridUser.Items.Add(new DataGridUserCols { FileNumber = int.Parse(bookNumber.ToString()) + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Failure" });
                    });
                    LogLine("FAILURE", dr.FileName + " Cover generation failed");
                }
                Dispatcher.Invoke(() =>
                {
                    DataGridUser.ScrollIntoView(DataGridUser.Items.GetItemAt(DataGridUser.Items.Count - 1));
                    progressBar.Value++;
                });
                Thread resize = new(ResizeThread);
                resize.Start();
            }
            if (bookFailure > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    resultLabel.Content = Strings.Failure + bookFailure.ToString() + Strings.Of + bookSuccess.ToString() + Strings.Covers;
                });
                LogLine("FAILURE", "Cover generation progress was finished with errors)");
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    resultLabel.Content = Strings.Generated + bookSuccess.ToString() + Strings.Covers + Strings.CanTransfer + bookTransferible;
                    resultLabel.ToolTip = Strings.NotTransferible;
                });
                LogLine("SUCCESS", "Cover generation progress was finished successfully");
            }
            Thread resizeEnd = new(ResizeThread);
            resizeEnd.Start();
            Dispatcher.Invoke(() =>
            {
                progressBar.Value++;
                runningNow.Content = Strings.Finished;
                transferButton.Visibility = System.Windows.Visibility.Visible;
                if (connectedDevice.Content.ToString()!.Contains(Strings.KindleOther) || connectedDevice.Content.ToString()!.Contains(Strings.KindleScribe))
                {
                    if (bookTransferible > 0)
                    {
                        transferButton.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show(Strings.CantTransfer, Strings.CantTransferTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(Strings.KindleNotconnected, Strings.KindleNone, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
    }
}
