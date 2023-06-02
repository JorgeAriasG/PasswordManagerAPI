using password_manager.api.Models;

namespace password_manager.api.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User user, Guid id);
        bool SaveChanges();
    }
}