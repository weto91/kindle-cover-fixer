using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        // Check the version in GitHub and compare it with the application version
        private async void CheckGitHubNewerVersion()
        {
            GitHubClient client = new(new ProductHeaderValue("Kindle_Cover_Fixer"));
            var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");
            var latest = releases.Name;
            var features = releases.Body;
            float latestVersion = float.Parse(latest.ToString().Split(' ')[1]);
            float localVersion = float.Parse(UsefulVariables.AppVersion);
            if (latestVersion > localVersion)
            {
                versionApp.Content = "Version " + UsefulVariables.AppVersion + " (New version available)";
                MessageBoxResult result = MessageBox.Show("Do you want to download and install the new version? \r\n \r\n You will get the following features: \r\n \r\n." + features, "New version available!.", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start("explorer", "https://github.com/weto91/kindle-cover-fixer/releases/latest");
                    this.Close();
                }
            }
            else
            {
                versionApp.Content = "Version " + UsefulVariables.AppVersion + " (Up to date)";
            }
        }
    }
}
