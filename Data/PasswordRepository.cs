using password_manager.api.Interfaces;
using password_manager.api.Models;

namespace password_manager.api.Data
{
    public class PasswordRepository : IPassword
    {
        private readonly PasswordManagerContext _context;
        public PasswordRepository(PasswordManagerContext context)
        {
            _context = context;
        }

        public void CreatePassword(Password password)
        {
            // TODO: Assign values to UserId, Created
            _context.Password.Add(password);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}