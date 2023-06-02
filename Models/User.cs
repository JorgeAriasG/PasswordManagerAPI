using System;
using System.ComponentModel.DataAnnotations;

namespace password_manager.api.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
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
        [Required]
        public string AccountLevel { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
        [Required]
        public bool IsEnabled { get; set; }
    }
}