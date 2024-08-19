using ControlerUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlerUsers.Data
{
    public class UserContext(DbContextOptions<UserContext> options): DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
