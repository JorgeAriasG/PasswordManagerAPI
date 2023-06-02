using System.ComponentModel.DataAnnotations;

namespace password_manager.api.Dtos.Password
{
    public class CreatePasswordDto
    {
        [Required]
        public string Site { get; set; }
        public string Username { get; set; }
        [Required]
        public string Pword { get; set; }
    }
}