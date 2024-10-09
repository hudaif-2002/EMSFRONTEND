
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EMSFRONTEND.Models
{
  

   
        public class LeaveRequestModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int LeaveId { get; set; }

            [ForeignKey("Users")]
            public int UserId { get; set; }

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

            [StringLength(500)]
            public string Reason { get; set; }

            [Required]
            [StringLength(20)]
            [DefaultValue("Pending")]
        public string LeaveStatus { get; set; } = "Pending"; // Default value

            // Navigation property
           // public virtual UsersModel User { get; set; }
        }
    }

