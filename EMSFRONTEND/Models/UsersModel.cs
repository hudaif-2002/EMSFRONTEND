using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EMSFRONTEND.Models
{
   
        public class UsersModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }

            [Required]
            [StringLength(100)]
            public string Username { get; set; }

            [Required]
            [StringLength(256)]
            public string Password { get; set; }

            [Required]
            [EmailAddress]
            [StringLength(256)]
            public string Email { get; set; }

            [StringLength(150)]
            public string FullName { get; set; }

            [Required]
            [StringLength(50)]
            public string Role { get; set; }

            [StringLength(150)]
            public string ManagerName { get; set; } = "Admin";
        public int? ManagerId { get; set; } = 1001;

        public DateTime DateOfJoining { get; set; }  
    }
    }

