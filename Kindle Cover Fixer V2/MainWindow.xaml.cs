using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Kindle_Cover_Fixer_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {       
        string StateDevice = "DSCON";
        public MainWindow()
        {
            InitializeComponent();
        }
        // Run when generateButton was clicked
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            CoverGenerationTask();
        }      
        // Find the books on selected Calibre library
        private void FindBooks_Click(object sender, RoutedEventArgs e)
        {
            FindBooksTask();
          //  if (checkSync.Content.ToString()!.Contains(Strings.GeneratingError))
          //  {
              checkSync.Content = Strings.CheckSync;
          //  }
        }
        // Log when the app closing
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogLine("INFO", "The application is closing. Bye!");
        }

        private void CheckSync_Enter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            checkSync.Foreground = Brushes.MediumBlue;
        }

        private void CheckSync_Leave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            checkSync.Foreground = Brushes.DarkBlue;
        }

        private void checkSync_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: Comprobar desde el datagrid, los UUID con respecto al metadata.json de la raiz del Kindle
        }

        private void SaveTable_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Guardar el datagrid como tabla .csv
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearKindle_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Crear función para limpiar la carpeta del kindle de covers no utilizados
        }

        private void ClearOutput_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Eliminar las imágenes del output folder
        }

        private void FindUnsynced_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Buscar libros en la libreria y kindle, que no coincidan UUIDs
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Link a la Wiki de GH
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Link al perfil de GH
        }
    }
}
