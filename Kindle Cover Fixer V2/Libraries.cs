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
                LogLine("INFO", "Library: " + library + " loaded.");
            }
            libraryPath.SelectedIndex = 0;
        }
    }
}
