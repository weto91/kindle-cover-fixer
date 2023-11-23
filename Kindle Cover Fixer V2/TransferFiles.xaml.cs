using System;
using System.Windows;

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
            closeBut.Content = Strings.Close;
            transferBut.Content = Strings.Transfer;
            TitleTransfer.Content = Strings.TransTitle;
            CanDoThings.Content = Strings.CanDo;
            ThingOne.Content = Strings.ThingOne;
            ThingTwo.Content = Strings.ThingTwo;
            ThingAclaration.Content = Strings.Aclaration;
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
