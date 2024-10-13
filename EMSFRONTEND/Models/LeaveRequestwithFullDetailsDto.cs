using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EMSFRONTEND.Models
{
    public class LeaveRequestwithFullDetailsDto
    {
 
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int LeaveId { get; set; }

            [ForeignKey("Users")]
            public int UserId { get; set; }
            public string FullName { get; set; }

            public DateOnly FromDate { get; set; }
            public DateOnly ToDate { get; set; }

            [StringLength(500)]
            public string Reason { get; set; }

            [Required]
            [StringLength(20)]
            [DefaultValue("Pending")]
            public string LeaveStatus { get; set; } = "Pending"; // Default value
        }
}
