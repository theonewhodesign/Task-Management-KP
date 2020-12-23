using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.API.Dtos;
using TaskMangement.Models;

namespace TaskManagement.API.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ReadTaskDto>> GetTasks();
        Task<ReadTaskDto> GetTask(int taskId);
        Task<UserTask> CreateTask(CreateTaskDto createTaskDto);
        Task<UserTask> UpdateTask(int taskId, UpdateTaskDto updateTaskDto);
        Task<UserTask> DeleteTask(int taskId);
    }
}
