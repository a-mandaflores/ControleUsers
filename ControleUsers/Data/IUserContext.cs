
using ControlerUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlerUsers.Data
{
    public interface IUserContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}