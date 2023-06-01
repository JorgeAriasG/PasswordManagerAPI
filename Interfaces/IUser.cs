using password_manager.api.Models;

namespace password_manager.api.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        bool SaveChanges();
    }
}