namespace Kindle_Cover_Fixer_V2
{
    public struct DataGridUserCols
    {
        public string FileNumber { get; set; }
        public string FileName { get; set; }
        public string FileUuid { get; set; }
        public string FileCan {  get; set; }
    }
    public struct DataGridSystemCols
    {
        public int FileNumber { get; set; }
        public string FilePath { get; set; }
        public string FileUuid { get; set; }
        public string FileName { get; set; }
    }
    public struct DataGridTransferCols
    {
        public string FileNumber { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}
