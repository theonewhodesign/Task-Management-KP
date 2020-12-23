using Microsoft.EntityFrameworkCore;
using TaskMangement.Models;

namespace TaskManagement.API.Repository
{
    public class TaskManagementDBContext : DbContext
    {
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<UserTask> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
