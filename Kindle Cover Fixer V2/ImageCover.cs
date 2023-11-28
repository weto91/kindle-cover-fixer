using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Threading;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private void loadImages()
        {
            List<string> libraryList = new List<string>();
            Dispatcher.Invoke(() =>
            {
                if (libraryPath.Items.Count > 0)
                {
                    foreach (String lib in libraryPath.Items)
                    {
                        if (lib != Strings.AllLibraries)
                        {
                            libraryList.Add(lib);
                        }                     
                    }
                }              
            });
            while (true)
            {
                Random r = new Random();
                int rInt = r.Next(0, libraryList.Count);
                string library = libraryList[rInt];
                {
                    if (library.Length > 1)
                    {
                        List<string> data = new();
                        string cs = @"URI=file:" + library + @"\metadata.db";
                        using SQLiteConnection connection = new(cs);
                        connection.Open();
                        using (SQLiteCommand selectCMD = connection.CreateCommand())
                        {
                            selectCMD.CommandText = "SELECT * FROM books ORDER BY RANDOM() LIMIT 1;";
                            selectCMD.CommandType = CommandType.Text;
                            SQLiteDataReader myReader = selectCMD.ExecuteReader();
                            while (myReader.Read())
                            {
                                string bookPath = library + @"\" + myReader["path"].ToString() + @"\cover.jpg";
                                if (File.Exists(bookPath))
                                {
                                    data.Add(bookPath);
                                }
                            }
                        }
                        foreach (string book in data)
                        {
                            Trace.WriteLine("Cover: " + book);
                            Dispatcher.Invoke(() =>
                            {
                                coverShow.Source = new BitmapImage(new Uri(book));
                            });
                            Thread.Sleep(8000);
                        }
                    }
                }
            }
        }
    }
}
