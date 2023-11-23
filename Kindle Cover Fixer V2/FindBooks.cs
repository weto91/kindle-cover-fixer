using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kindle_Cover_Fixer_V2
{
    // this file have the functions about the book finder in your library
    public partial class MainWindow
    {
        private void FindBooksTask()
        {
            resultLabel.Content = String.Empty;
            runningNow.Content = "Finding books in your library...";
            //string CalibreLibrary = libraryPath.Text;
            DataGridUser.Items.Clear();
            DataGridSystem.Items.Clear();
            string cs = @"URI=file:" + libraryPath.Text + @"\metadata.db";
            using SQLiteConnection connection = new(cs);
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT * FROM books";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                int bookCounter = 1;
                int bookNotPassed = 0;
                while (myReader.Read())
                {
                    string canDoIt = "false";
                    string bookPath = libraryPath.Text + @"\" + myReader["path"].ToString();
                    string? bookUuid = myReader["uuid"].ToString();
                    string? bookTitle = myReader["title"].ToString();
                    if (myReader["uuid"].ToString()!.Length >= 10 && myReader["has_cover"].ToString() == "True")
                    {
                        canDoIt = "true";
                        DataGridSystem.Items.Add(new DataGridSystemCols { FileNumber = bookCounter, FileName = bookTitle!, FilePath = bookPath, FileUuid = bookUuid! });
                        LogLine("INFO", "Book: " + bookTitle! + " loaded. Can be generated.");
                    }
                    else
                    {
                        bookNotPassed++;
                        LogLine("INFO", "Book: " + bookTitle! + " loaded. Can't be generated.");
                    }
                    DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookCounter.ToString(), FileName = bookTitle!, FileUuid = bookUuid!, FileCan = canDoIt });
                    bookCounter++;
                }
                if (bookNotPassed > 0)
                {
                    resultLabel.Content = @"Can't generate: " + bookNotPassed.ToString() + " of " + DataGridSystem.Items.Count + " cover(s)";
                    LogLine("FAILURE", "Book listing process finished with errors");
                }
                else
                {
                    resultLabel.Content = @"All cover(s) can be generated, total: " + DataGridSystem.Items.Count + " cover(s)";
                    LogLine("SUCCESS", "Book: listing process finished successfully");
                }
            }
            connection.Close();
            Thread resize = new(ResizeThread);
            resize.Start();
            runningNow.Content = "All book listed";
            EnableControl(generateButton);
        }
    }
}
