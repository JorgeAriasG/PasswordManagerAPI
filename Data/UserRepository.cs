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

        public User GetUserById(Guid userId)
        {
            return _context.User.FirstOrDefault<User>(u => u.Id == userId);
        }

        public void CreateUser(User user)
        {
            _context.Add(user);
        }

        public void DeleteUser(User user)
        {
            _context.User.Remove(user);
        }

        public User LoginUser(User user)
        {
            return _context.User.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}