using ControlerUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlerUsers.Data;

public class UserContext(DbContextOptions<UserContext> options)
    : DbContext(options),
    IUserContext
{
    public DbSet<User> Users { get; set; }
}