using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private void test()
        {
            Trace.WriteLine(GetPathForExe("ebook-convert.exe"));
        }
        private string GetPathForExe(string fileName)
        {
            string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey fileKey = localMachine.OpenSubKey(string.Format(@"{0}\{1}", keyBase, fileName))!;
            object result = null!;
            if (fileKey != null)
            {
                result = fileKey.GetValue(string.Empty)!;
                fileKey.Close();
            }
            return (string)result;
        }

    }
}
