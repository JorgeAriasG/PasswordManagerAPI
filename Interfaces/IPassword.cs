using password_manager.api.Models;

namespace password_manager.api.Interfaces
{
    public interface IPassword
    {
        void CreatePassword(Password password);
        IEnumerable<Password> GetAllPasswords(Guid userId);
        public Password GetPasswordById(Guid userId, int id);
        void DeletePassword(Password password);
        bool SaveChanges();

    }
}