using System.ComponentModel.DataAnnotations;

namespace EMSFRONTEND.Models
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }  // Add this field for the role (Employee/Manager)
    }


}
