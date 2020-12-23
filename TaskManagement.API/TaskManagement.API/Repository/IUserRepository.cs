using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMangement.Models;

namespace TaskManagement.API.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int userId, User user);
        Task<User> DeleteUser(int userId);
    }
}
