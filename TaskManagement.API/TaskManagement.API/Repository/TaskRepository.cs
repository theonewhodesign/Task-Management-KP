using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.API.Dtos;
using TaskMangement.Models;
using TaskStatus = TaskMangement.Models.TaskStatus;

namespace TaskManagement.API.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDBContext _taskManagementDBContext;

        public TaskRepository(TaskManagementDBContext taskManagementDBContext)
        {
            _taskManagementDBContext = taskManagementDBContext;
        }
        public async Task<UserTask> CreateTask(CreateTaskDto createTaskDto)
        {
            User userResult;
            if (createTaskDto.UserId == 0)
            {
                userResult = new User()
                {
                    UserId = 0,
                    FirstName = null,
                    LastName = null
                };
            }
            else
            {
                userResult = await _taskManagementDBContext.Users.FirstOrDefaultAsync(x => x.UserId == createTaskDto.UserId);
            }
            UserTask userTask = new UserTask()
            {
                TaskName = createTaskDto.TaskName,
                TaskDescription = createTaskDto.TaskDescription,
                StartDate = createTaskDto.StartDate,
                EndDate = createTaskDto.EndDate,
                IsOpen = true,
                Status = TaskStatus.Active.ToString(),
                User = userResult
            };
            var taskResult = await _taskManagementDBContext.Tasks.AddAsync(userTask);
            _taskManagementDBContext.SaveChanges();
            return taskResult.Entity;
        }

        public async Task<UserTask> DeleteTask(int taskId)
        {
            var taskResult = await _taskManagementDBContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId);
            _taskManagementDBContext.Remove(taskResult);
            _taskManagementDBContext.SaveChanges();
            return taskResult;
        }

        public async Task<ReadTaskDto> GetTask(int taskId)
        {
            return await _taskManagementDBContext.Tasks.Select(s => new ReadTaskDto()
            {
                TaskId = s.TaskId,
                TaskName = s.TaskName,
                TaskDescription = s.TaskDescription,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsOpen = s.IsOpen,
                Status = s.Status,
                UserId = s.User.UserId,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName
            }).FirstOrDefaultAsync(x => x.TaskId == taskId);
        }

        public async Task<IEnumerable<ReadTaskDto>> GetTasks()
        {
            var abc = await _taskManagementDBContext.Tasks.Select(s => new ReadTaskDto()
            {
                TaskId = s.TaskId,
                TaskName = s.TaskName,
                TaskDescription = s.TaskDescription,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsOpen = s.IsOpen,
                Status = s.Status,
                UserId = s.User != null ? s.User.UserId : 0,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName
            }).ToListAsync();

            return abc;
        }

        public async Task<UserTask> UpdateTask(int taskId, UpdateTaskDto updateTaskDto)
        {
            var taskResult = await _taskManagementDBContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId);
            User user = await _taskManagementDBContext.Users.FirstOrDefaultAsync(x => x.UserId == updateTaskDto.UserId);
            if (taskResult != null)
            {
                taskResult.TaskName = updateTaskDto.TaskName;
                taskResult.TaskDescription = updateTaskDto.TaskDescription;
                taskResult.StartDate = updateTaskDto.StartDate;
                taskResult.EndDate = updateTaskDto.EndDate;
                taskResult.IsOpen = updateTaskDto.IsOpen;
                taskResult.Status = updateTaskDto.Status;
                taskResult.User = user;
                _taskManagementDBContext.SaveChanges();
            }
            return taskResult;
        }
    }
}

