using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kindle_Cover_Fixer_V2
{
    /// <summary>
    /// Lógica de interacción para TransferFiles.xaml
    /// </summary>
    public partial class TransferFiles : Window
    {
        public string action = string.Empty;
        public TransferFiles()
        {
            InitializeComponent();
        }

        private void TransferFiles_Rendered(object sender, EventArgs e)
        {
            //this.Width = closeBut.Width + openBut.Width + transferBut.Width +
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void openOutput_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void transferFiles_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
