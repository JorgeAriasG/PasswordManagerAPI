namespace password_manager.api.Dtos
{
    public class UpdateUserDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}