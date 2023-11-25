using System.IO;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private static void CleanOrCreateOutput()
        {
            if (Directory.Exists(UsefulVariables.OutputFolder()))
            {
                // Delete content
                foreach (string file in Directory.GetFiles(UsefulVariables.OutputFolder()))
                {
                    File.Delete(file);
                }
            }
            else
            {
                // Create directory
                Directory.CreateDirectory(UsefulVariables.OutputFolder());
            }
        }
    }
}
