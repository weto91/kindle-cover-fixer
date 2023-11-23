using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Kindle_Cover_Fixer_V2
{
    // This file have the functions to generate the cover files.
    public partial class MainWindow
    {
        private void CoverGenerationTask()
        {
            resultLabel.Content = String.Empty;
            runningNow.Content = "Generating new covers...";
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
                resultLabel.Content = @"Failure on: " + bookFailure.ToString() + " of " + bookSuccess.ToString() + " cover(s)";
                LogLine("FAILURE", "Cover generation progress was finished with errors)");
            }
            else
            {
                Trace.WriteLine("ALL GENERATED");
                resultLabel.Content = @"All cover(s) generated, total: " + bookSuccess.ToString() + " cover(s)";
                LogLine("SUCCESS", "Cover generation progress was finished successfully");
            }
            Thread resize = new(ResizeThread);
            resize.Start();
            progressBar.Value++;
            runningNow.Content = "Job finished";
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
