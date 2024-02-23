using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
