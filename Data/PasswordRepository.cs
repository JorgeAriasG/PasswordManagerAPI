using System.Linq;
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
            // TODO: Assign values to UserId, Created using session data like cookies.
            _context.Password.Add(password);
        }

        public IEnumerable<Password> GetAllPasswords(Guid userId)
        {
            return _context.Password.Where(p => p.UserId == userId).ToArray();
        }

        public Password GetPasswordById(Guid userId, int id)
        {
            return _context.Password
                .Where(p => p.Id == id && p.UserId == userId).FirstOrDefault();
        }

        public void DeletePassword(Password password)
        {
            _context.Password.Remove(password);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}