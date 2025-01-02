namespace MultimediaManagementBackend.Models
{
    public class FileMetadata
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Category { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }
        public string Tags { get; set; }
    }
}
