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
            user.Id = Guid.NewGuid();
            user.AccountLevel = "Master";
            user.Created = DateTimeOffset.Now;
            user.IsEnabled = true;
            _context.Add(user);
        }

        public void UpdateUser(User user, Guid id)
        {
            User userToUpdate = GetUserById(id);

            if (userToUpdate != null)
            {
                userToUpdate = user;
            }
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}