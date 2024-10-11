using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMSFRONTEND.Models
{
 
   
        public class TaskModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int TaskId { get; set; }

            [ForeignKey("Users")]
            public int UserId { get; set; }

            [Required]
            [StringLength(150)]
            public string? Title { get; set; }

            public DateTime AssignedDate { get; set; }  // Defaults to current date and time

            public DateTime DeadlineDate { get; set; }

            [StringLength(500)]
            public string Description { get; set; }

            [Required]
            [StringLength(20)]
            [DefaultValue("Pending")]
            public string TaskStatus { get; set; } = "Pending"; // Default value

            [StringLength(255)] // Adjust length as necessary for OneDrive links
            public string Resources { get; set; } // OneDrive link

            // Navigation property
            //public virtual UsersModel User { get; set; }
        }
    }

