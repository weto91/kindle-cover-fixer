﻿using Octokit;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private async void CheckGitHubNewerVersion()
        {
            GitHubClient client = new(new ProductHeaderValue("Kindle_Cover_Fixer"));
            var releases = await client.Repository.Release.GetLatest("weto91", "kindle-cover-fixer");
            var latest = releases.Name;
            var features = releases.Body;
            float latestVersion = float.Parse(latest.ToString().Split(' ')[1], CultureInfo.InvariantCulture);
            float localVersion = float.Parse(UsefulVariables.AppVersion, CultureInfo.InvariantCulture);
            if (latestVersion > localVersion)
            {
                versionApp.Content = Strings.Version + UsefulVariables.AppVersion + Strings.NewVersionAv; 
                MessageBoxResult result = MessageBox.Show(Strings.NewVersionWant + features, Strings.NewVersionWantTitle, MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start("explorer", "https://github.com/weto91/kindle-cover-fixer/releases/latest");
                    this.Close();
                }
            }
            else
            {
                versionApp.Content = Strings.Version + UsefulVariables.AppVersion + Strings.UpToDate;
            }
        }
    }
}
