using System.IO;

namespace Kindle_Cover_Fixer_V2
{
    public partial class Settings
    {
        private void ChangeSettings()
        {
            string? optimize = "Optimize: " + optimizeCheck.IsChecked.ToString() + "\r\n";
            string? grayscale = "GrayScale: " + grayScaleCheck.IsChecked.ToString() + "\r\n";
            string settingsFile = optimize + grayscale;
            string settingsPath = UsefulVariables.GetKindleCoverFixerPath() + @"\settings.conf";
            File.WriteAllText(settingsPath, settingsFile);
        }
    }
}
