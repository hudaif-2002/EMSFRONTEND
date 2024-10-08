
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EMSFRONTEND.Models
{
    

    
        public class UploadModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int UploadId { get; set; }

            [ForeignKey("Tasks")]
            public int TaskId { get; set; }

            [ForeignKey("Users")]
            public int UserId { get; set; }

            [StringLength(255)] // Adjust length as necessary for OneDrive links
            public string FilePath { get; set; } // OneDrive link

            public DateTime UploadedAt { get; set; } = DateTime.UtcNow; // Defaults to current date and time

            // Navigation properties
           // public virtual TaskModel Task { get; set; }
            //public virtual UsersModel User { get; set; }
        }
    }

