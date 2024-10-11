
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EMSFRONTEND.Models
{


        public class PerformanceModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int PerformanceId { get; set; }

            [ForeignKey("Users")]
            public int UserId { get; set; }

            public int NumberOfTasksAssigned { get; set; } // Calculated count of tasks assigned to the user
            public int CompletedCount { get; set; } // Count of completed tasks

            [NotMapped] // This property will not be mapped to the database
            public double PerformancePercentage
            {
                get
                {
                    if (NumberOfTasksAssigned == 0) return 0; // Avoid division by zero
                    return Math.Round((double)CompletedCount / NumberOfTasksAssigned * 100, 2);
            }
            }

            // Navigation property
           // public virtual UsersModel User { get; set; }
        }
    }

