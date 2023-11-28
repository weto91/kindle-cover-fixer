using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        // Add to ComboBox the available libraries on your computer
        private void ListAllLibraries()
        {
            libraryPath.Items.Clear();
            foreach (string library in UsefulVariables.CalibreLibraries())
            {
                if (!library.Contains("[ERROR]"))
                {
                    libraryPath.Items.Add(library);
                }
                else
                {
                    DisableControl(findBooks);
                    DisableControl(generateButton);
                    MessageBox.Show(Strings.CheckLibrary, Strings.CheckLibraryTitle, MessageBoxButton.OK, MessageBoxImage.Error); 
                }
                LogLine("LIBRARY", "Library: " + library + " loaded.");
            }
            if (libraryPath.Items.Count > 1) 
            {
                // TODO: change when finish the develop of this: libraryPath.Items.Insert(0, Strings.AllLibraries); 
                libraryPath.Items.Add(Strings.AllLibraries);
            }
            libraryPath.SelectedIndex = 0;
        }

    }
}
