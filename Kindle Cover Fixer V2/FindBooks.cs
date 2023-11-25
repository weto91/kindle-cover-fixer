using System;
using System.Data.SQLite;
using System.Data;
using System.Threading;
using System.Windows.Controls;

namespace Kindle_Cover_Fixer_V2
{
    // this file have the functions about the book finder in your library
    public partial class MainWindow
    {
        private void FindBooksTask()
        {
            string library = string.Empty;
            Dispatcher.Invoke(() =>
            {
                transferButton.IsEnabled = false;
                generateButton.IsEnabled = false;
                DataGridUserPreparation();
                library = libraryPath.Text;
                resultLabel.Content = String.Empty;
                runningNow.Content = Strings.Finding;
                DataGridUser.Items.Clear();
                DataGridSystem.Items.Clear();
            });
            string cs = @"URI=file:" + library + @"\metadata.db";
            using SQLiteConnection connection = new(cs);
            connection.Open();
            using (SQLiteCommand selectCMD = connection.CreateCommand())
            {
                selectCMD.CommandText = "SELECT COUNT(*) FROM books";
                Dispatcher.Invoke(() =>
                {
                    progressBar.Value = 0;
                    progressBar.Maximum = int.Parse(selectCMD.ExecuteScalar().ToString()!);
                });              
                selectCMD.CommandText = "SELECT * FROM books";
                selectCMD.CommandType = CommandType.Text;
                SQLiteDataReader myReader = selectCMD.ExecuteReader();
                int bookCounter = 1;
                while (myReader.Read())
                {
                    string canDoIt = String.Empty;
                    string bookPath = library + @"\" + myReader["path"].ToString();
                    string? bookUuid = myReader["uuid"].ToString();
                    string? bookTitle = myReader["title"].ToString();
                        if (IsOnKindle(bookUuid!, bookCounter))
                        {
                            canDoIt = Strings.Yes;                     
                        }
                        else
                        {
                            canDoIt = Strings.No;
                                                   
                        }
                    LogLine("LIST", bookUuid +  " | " + bookTitle! + " | Transferable: " + canDoIt);
                    Dispatcher.Invoke(() =>
                    {
                        DataGridSystem.Items.Add(new DataGridSystemCols { FileNumber = bookCounter, FileName = bookTitle!, FilePath = bookPath, FileUuid = bookUuid!, FileCan = canDoIt });
                        DataGridUser.Items.Add(new DataGridUserCols { FileNumber = bookCounter.ToString(), FileName = bookTitle!, FileUuid = bookUuid!, FileCan = canDoIt });
                        DataGridUser.ScrollIntoView(DataGridUser.Items.GetItemAt(DataGridUser.Items.Count - 1));
                    });              
                    bookCounter++;
                    Thread resize = new(ResizeThread);
                    resize.Start();
                    Dispatcher.Invoke(() =>
                    {
                        progressBar.Value++;
                    });
                }
                Dispatcher.Invoke(() =>
                {
                    progressBar.Value++;
                    resultLabel.Content = Strings.FinishListing;
                });                   
                LogLine("SUCCESS", "Book listing process finished.");
            }
            connection.Close();
            Thread resizeEnd = new(ResizeThread);
            resizeEnd.Start();
            Dispatcher.Invoke(() => 
            {
                runningNow.Content = Strings.BookListed;
                EnableControl(generateButton);
            });            
        }
    }
}
