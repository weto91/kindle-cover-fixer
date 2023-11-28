using System.Windows.Controls;
using System.Windows.Data;

namespace Kindle_Cover_Fixer_V2
{
    public struct DataGridUserCols
    {
        public string FileNumber { get; set; }
        public string FileName { get; set; }
        public string FileUuid { get; set; }
        public string FileCan {  get; set; }
    }
    public struct DataGridFindBooks
    {
        public string FileNumber { get; set; }
        public string FileName { get; set; }
        public string FileUuid { get; set; }
        public string FileProblems { get; set; }
        public string FileInKindle { get; set; }
    }
    public struct DataGridSystemCols
    {
        public int FileNumber { get; set; }
        public string FilePath { get; set; }
        public string FileUuid { get; set; }
        public string FileName { get; set; }
        public string FileCan { get; set; }
    }
    public struct DataGridTransferCols
    {
        public string FileNumber { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
    
    public partial class MainWindow
    {        
        // Define the datagrid of the FindBooks result
        private void InitDataGridFindBooks()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            DataGridTextColumn col3 = new();
            DataGridTextColumn col4 = new();
            // Columns creation
            DataGridUser.Columns.Add(col0);
            DataGridUser.Columns.Add(col1);
            DataGridUser.Columns.Add(col2);
            DataGridUser.Columns.Add(col3);
            DataGridUser.Columns.Add(col4);
            // Column header configuration
            col0.Header = Strings.BookNumber;
            col1.Header = Strings.BookName;
            col2.Header = Strings.BookUuid;
            col3.Header = Strings.BookPassed;
            col4.Header = Strings.BookProblem;           
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileInKindle");
            col4.Binding = new Binding("FileProblems"); 
        }
        // Define the User information DataGrid structure for Generate covers
        private void DataGridUserPreparationGen()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            DataGridTextColumn col3 = new();
            // Columns creation
            DataGridUser.Columns.Add(col0);
            DataGridUser.Columns.Add(col1);
            DataGridUser.Columns.Add(col2);
            DataGridUser.Columns.Add(col3);
            // Column header configuration
            col0.Header = Strings.BookNumber;
            col1.Header = Strings.BookName;
            col2.Header = Strings.BookUuid;
            col3.Header = Strings.BookStatus;
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the User information DataGrid structure for checks Calibre
        private void DataGridUserPreparationCalibre()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            DataGridTextColumn col3 = new();
            // Columns creation
            DataGridUser.Columns.Add(col0);
            DataGridUser.Columns.Add(col1);
            DataGridUser.Columns.Add(col2);
            DataGridUser.Columns.Add(col3);
            // Column header configuration
            col0.Header = Strings.BookNumber;
            col1.Header = Strings.BookName;
            col2.Header = Strings.BookUuid;
            col3.Header = Strings.BookProblem;
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the User information DataGrid structure for checks Calibre
        private void DataGridUserPreparationKindle()
        {
            DataGridUser.Items.Clear();
            DataGridUser.Columns.Clear();
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            DataGridTextColumn col3 = new();
            // Columns creation
            DataGridUser.Columns.Add(col0);
            DataGridUser.Columns.Add(col1);
            DataGridUser.Columns.Add(col2);
            DataGridUser.Columns.Add(col3);
            // Column header configuration
            col0.Header = Strings.BookNumber;
            col1.Header = Strings.BookName;
            col2.Header = Strings.BookLibrary;
            col3.Header = Strings.BookProblem;
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileUuid");
            col3.Binding = new Binding("FileCan");
        }
        // Define the System information DataGrid structure
        private void DataGridSystemPreparation()
        {
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            // Columns creation
            DataGridSystem.Columns.Add(col0);
            DataGridSystem.Columns.Add(col1);
            DataGridSystem.Columns.Add(col2);
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FilePath");
            col2.Binding = new Binding("FileUuid");
        }
        // Define the transfer information DataGrid structure
        private void DataGridTransferPreparation()
        {
            // Columns definition
            DataGridTextColumn col0 = new();
            DataGridTextColumn col1 = new();
            DataGridTextColumn col2 = new();
            // Columns creation
            DataGridTransfer.Columns.Add(col0);
            DataGridTransfer.Columns.Add(col1);
            DataGridTransfer.Columns.Add(col2);
            // Bindings with the struct
            col0.Binding = new Binding("FileNumber");
            col1.Binding = new Binding("FileName");
            col2.Binding = new Binding("FileSize");
        }
    }
}
