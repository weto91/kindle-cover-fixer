using System.Data.SQLite;
using System.Data;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;

namespace Kindle_Cover_Fixer_V2
{
    // this file have the functions about the book finder in your library
    public partial class MainWindow
    {
        private void FindBooksTask()
        {
            List<string> libraries = new();
            IsOnKindlePreparation();
            Dispatcher.Invoke(() =>
            {
                if(libraryPath.Text != Strings.AllLibraries)
                {
                    libraries.Add(libraryPath.Text);
                }
                else
                {
                    foreach (string lib in libraryPath.Items)
                    {
                        libraries.Add(lib);
                    }
                }
                transferButton.IsEnabled = false;
                generateButton.IsEnabled = false;
                transferButton.IsEnabled = false;
                InitDataGridFindBooks();
                DataGridSystem.Items.Clear();
                resultLabel.Content = string.Empty;
                runningNow.Content = Strings.Finding;               
            });
            foreach (string library in libraries)
            {
                if (File.Exists(library + @"\metadata.db"))
                {
                    File.Copy(library + @"\metadata.db", UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.db", true);
                    string cs = @"URI=file:" + UsefulVariables.GetKindleCoverFixerPath() + @"\metadata.db";
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
                        int errorCounter = 0;
                        while (myReader.Read())
                        {
                            string InKindle = string.Empty;
                            string problems = string.Empty;
                            string bookPath = library + @"\" + myReader["path"].ToString();
                            string uuid = myReader["uuid"].ToString()!;
                            string[] bookUuid = RealBookUuid(bookPath, uuid);
                            string bookTitle = myReader["title"].ToString()!;
                            bool checkError = false;
                            if (IsOnKindle(uuid, bookCounter))
                            {
                                InKindle = Strings.Yes;
                            }
                            else
                            {
                                checkError = true;
                                InKindle = Strings.No;
                            }
                            if (!File.Exists(bookPath + @"\cover.jpg"))
                            {
                                checkError = true;
                                problems = "COVER";
                            }
                            if (bookUuid[0].Length != 36)
                            {
                                if (problems.Contains("COVER"))
                                {
                                    problems += ", ";
                                }
                                checkError = true;
                                problems += "UUID";
                            }
                            if (bookUuid[1].Contains("WF"))
                            {
                                if (problems.Length > 1)
                                {
                                    problems += ", ";
                                }
                                problems += "FORMAT";
                            }
                            if (checkError)
                            {
                                errorCounter++;
                                Dispatcher.Invoke(() =>
                                {
                                    formatExplain.Text = Strings.ErrorExplanation;
                                    formatExplain.Visibility = System.Windows.Visibility.Visible;
                                });
                            }
                            LogLine("LIST", uuid + " | " + bookTitle! + " | Transferable: " + InKindle + " | Errors: " + problems);
                            string generateIt = Strings.No;
                            Dispatcher.Invoke(() =>
                            {
                                if (!checkError)
                                {
                                    generateIt = Strings.Yes;
                                    DataGridSystem.Items.Add(new DataGridSystemCols { FileNumber = bookCounter, FileName = bookTitle!, FilePath = bookPath, FileUuid = bookUuid[0], FileCan = generateIt });
                                }
                                DataGridUser.Items.Add(new DataGridFindBooks { FileNumber = bookCounter.ToString(), FileName = bookTitle!, FileUuid = bookUuid[0], FileInKindle = InKindle, FileProblems = problems });
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
                        connection.Close();
                        Dispatcher.Invoke(() =>
                        {
                            progressBar.Value++;
                            if (errorCounter > 0)
                            {
                                resultLabel.Content = Strings.FinishListingWithErrors + errorCounter + Strings.Of + DataGridUser.Items.Count + Strings.HaveError;
                                LogLine("ERROR", "Book listing process finished with errors in: " + errorCounter + " of " + DataGridUser.Items.Count + "Books");
                            }
                            else
                            {
                                resultLabel.Content = Strings.FinishListing;
                                LogLine("SUCCESS", "Book listing process finished.");
                            }
                        });
                    }
                }
            }
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
