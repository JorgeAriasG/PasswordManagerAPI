using System.ComponentModel.DataAnnotations;

namespace password_manager.api.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}