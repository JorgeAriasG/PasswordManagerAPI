using System.ComponentModel.DataAnnotations;

namespace password_manager.api.Dtos.Password
{
    public class UpdatePasswordDto
    {
        public string Site { get; set; }
        public string Username { get; set; }
        public string Pword { get; set; }
    }
}