using ControlerUsers.Models;
using ControleUsers.DTOs;

namespace ControleUsers.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserPost> Create(UserPost user);
        Task<IEnumerable<UserRead>> GetAll();
        Task<UserRead> GetById(int id);
        Task Delete(int id);
    }
}
