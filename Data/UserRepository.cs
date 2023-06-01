using password_manager.api.Interfaces;
using password_manager.api.Models;

namespace password_manager.api.Data
{
    public class UserRepository : IUser
    {
        private readonly PasswordManagerContext _context;
        public UserRepository(PasswordManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public void CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.AccountLevel = "Master";
            user.Created = DateTimeOffset.Now;
            user.IsEnabled = true;
            _context.Add(user);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}