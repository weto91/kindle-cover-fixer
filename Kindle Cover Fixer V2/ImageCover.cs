using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private void loadImages()
        {
            string library = string.Empty;
            Dispatcher.Invoke(() =>
            {
                if (libraryPath.Items.Count > 0)
                {
                    library = libraryPath.SelectedItem.ToString()!;
                }              
            });
            if (library.Length > 1)
            {
                List<string> data = new();
                string cs = @"URI=file:" + library + @"\metadata.db";
                using SQLiteConnection connection = new(cs);
                connection.Open();
                using (SQLiteCommand selectCMD = connection.CreateCommand())
                {
                    selectCMD.CommandText = "SELECT * FROM books";
                    selectCMD.CommandType = CommandType.Text;
                    SQLiteDataReader myReader = selectCMD.ExecuteReader();
                    while (myReader.Read())
                    {
                        string bookPath = library + @"\" + myReader["path"].ToString() + @"\cover.jpg";
                        data.Add(bookPath);
                    }
                }
                while (true)
                {
                    foreach (string book in data)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            string libraryNew = libraryPath.SelectedItem.ToString()!;
                            if (library != libraryNew)
                            {
                                return;
                            }
                            coverShow.Source = new BitmapImage(new Uri(book));
                        });
                        Thread.Sleep(8000);
                    }
                }               
            }           
        }
    }
}
