using Microsoft.EntityFrameworkCore;
using password_manager.api.Models;

namespace password_manager.api.Data
{
    public class PasswordManagerContext : DbContext
    {
        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> opt) : base(opt)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Password> Password { get; set; }
    }
}