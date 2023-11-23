using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Kindle_Cover_Fixer_V2
{
    // This file have the functions to generate the cover files.
    public partial class MainWindow
    {
        private void CoverGenerationTask()
        {
            resultLabel.Content = String.Empty;
            runningNow.Content = Strings.Generating;
            progressBar.Maximum = DataGridSystem.Items.Count;
            progressBar.Value = 0;
            DataGridUser.Items.Clear();
            int bookFailure = 0;
            int bookSuccess = 0;
            foreach (DataGridSystemCols dr in DataGridSystem.Items)
            {
                double bookNumber = progressBar.Value + 1;
                string inputPath = dr.FilePath + @"\cover.jpg";
                if (!string.IsNullOrEmpty(inputPath) && !string.IsNullOrEmpty(dr.FilePath) && !string.IsNullOrEmpty(dr.FileUuid))
                {
                    String outputPath = UsefulVariables.OutputFolder() + @"\thumbnail_" + dr.FileUuid + @"_EBOK_portrait.jpg";
                    File.Copy(inputPath, outputPath, true);
                    if (File.Exists(outputPath))
                    {
                        bookSuccess++;
                        DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookNumber + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Cover generated" });
                        LogLine("SUCCESS", dr.FileName + " Cover generated");
                    }
                    else
                    {
                        bookFailure++;
                        DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookNumber + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Failure" });
                        LogLine("FAILURE", dr.FileName + " Cover generation failed");
                    }
                }
                else
                {
                    bookFailure++;
                    DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookNumber + @" of " + DataGridSystem.Items.Count, FileName = dr.FileName!, FileUuid = dr.FileUuid!, FileCan = "Failure" });
                    LogLine("FAILURE", dr.FileName + " Cover generation failed");
                }
                progressBar.Value++;
            }
            if (bookFailure > 0)
            {
                resultLabel.Content = Strings.Failure + bookFailure.ToString() + Strings.Of + bookSuccess.ToString() + Strings.Covers;
                LogLine("FAILURE", "Cover generation progress was finished with errors)");
            }
            else
            {
                Trace.WriteLine("ALL GENERATED");
                resultLabel.Content = Strings.Generated + bookSuccess.ToString() + Strings.Covers;
                LogLine("SUCCESS", "Cover generation progress was finished successfully");
            }
            Thread resize = new(ResizeThread);
            resize.Start();
            progressBar.Value++;
            runningNow.Content = Strings.Finished;
            TransferFiles transferFiles = new();
            bool? res = transferFiles.ShowDialog();
            if (res == true)
            {
                Thread task = new(TransferFilesToKindle);
                task.Start();
            }
        }
    }
}
