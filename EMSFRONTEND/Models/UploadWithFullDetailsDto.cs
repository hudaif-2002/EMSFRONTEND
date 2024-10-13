namespace EMSFRONTEND.Models
{
    public class UploadWithFullDetailsDto
    {

        public int UploadId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } // Add FullName here
        public string TaskName { get; set; } // Add TaskName (Title)
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
    }


}
