using password_manager.api.Models;

namespace password_manager.api.Interfaces
{
    public interface IPassword
    {
        void CreatePassword(Password password);
        bool SaveChanges();

    }
}