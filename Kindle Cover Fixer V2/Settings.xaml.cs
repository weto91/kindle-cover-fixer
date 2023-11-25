using System;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSettings();
            this.Close();               
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Settings_Initialized(object sender, EventArgs e)
        {
            optimizeCheck.IsChecked = UsefulVariables.Settings()[0];
            grayScaleCheck.IsChecked = UsefulVariables.Settings()[1];
        }
    }
}
