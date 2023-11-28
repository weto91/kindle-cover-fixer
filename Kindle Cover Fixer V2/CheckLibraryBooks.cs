namespace Kindle_Cover_Fixer_V2
{
    public partial class MainWindow
    {
        private void CheckCalibreBooks()
        {
            // TODO: Check if every book in the Calibre Library has UUID, if not: Last column in the datagrid shows: "Without UUID". If there is not any problem: "CORRECT"
        }
        private void CheckKindleBooks()
        {
            // TODO: Check if every book in kindle has UUID, also the UUID is on any of the listed libraries (put library name in the datagrid). If anything is wrong, show in the las column: "Without UUID" or "Not in libraries". Else, show: "CORRECT"
        }
    }
}
