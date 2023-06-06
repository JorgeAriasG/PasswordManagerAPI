using System.ComponentModel.DataAnnotations;

namespace password_manager.api.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}