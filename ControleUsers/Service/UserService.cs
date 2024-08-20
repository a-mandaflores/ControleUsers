using ControlerUsers.Data;
using ControlerUsers.Models;
using ControleUsers.DTOs;
using ControleUsers.Service.Interfaces;

namespace ControleUsers.Service;

public class UserService(IUserContext context) : IUserService
{
    public async Task<UserPost> Create(UserPost user)
    {
        var userDto = new User
        {
            Email = user.Email,
            Idade = user.Idade,
            Name = user.Name,
        };

        context.Users.Add(userDto);

        await context.SaveChangesAsync();

        return user;
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRead>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<UserRead> GetById(int id)
    {
        throw new NotImplementedException();
    }
}