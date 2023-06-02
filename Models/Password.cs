using System.ComponentModel.DataAnnotations;

namespace password_manager.api.Models
{
    public class Password
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Site { get; set; }
        public string Username { get; set; }
        [Required]
        public string Pword { get; set; }
        [Required]
        public DateTimeOffset Creted { get; set; }
        public DateTimeOffset Modified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}