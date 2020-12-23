using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskMangement.Models;

namespace TaskManagement.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementDBContext _taskManagementDBContext;

        public UserRepository(TaskManagementDBContext taskManagementDBContext)
        {
            _taskManagementDBContext = taskManagementDBContext;
        }
        public async Task<User> CreateUser(User user)
        {
            var userResult = await _taskManagementDBContext.Users.AddAsync(user);
            _taskManagementDBContext.SaveChanges();
            return userResult.Entity;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var userResult = await _taskManagementDBContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            _taskManagementDBContext.Remove(userResult);
            _taskManagementDBContext.SaveChanges();
            return userResult;
        }

        public async Task<User> GetUser(int userId)
        {
            return await _taskManagementDBContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _taskManagementDBContext.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(int userId, User user)
        {
            var userResult = await _taskManagementDBContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userResult != null)
            {
                userResult.FirstName = user.FirstName;
                userResult.LastName = user.LastName;
                _taskManagementDBContext.SaveChanges();
            }
            return userResult;
        }
    }
}
