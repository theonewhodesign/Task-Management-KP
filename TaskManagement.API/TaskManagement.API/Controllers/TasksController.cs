using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.API.Dtos;
using TaskManagement.API.Repository;
using TaskMangement.Models;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        #region CRUDOpration (Task)

        /// <summary>
        /// Get All Task
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadTaskDto>>> GetTasks()
        {
            try
            {
                return Ok(await _taskRepository.GetTasks());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving all task data from the database");
            }
        }

        /// <summary>
        /// Get Task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadTaskDto>> GetTask(int id)
        {
            var taskResult = await _taskRepository.GetTask(id);
            if (taskResult != null)
            {
                return Ok(taskResult);
            }
            else
            {
                return NotFound($"Task with Id - {id} not found in database");
            }
        }

        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name="userTask"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserTask>> CreateTask(CreateTaskDto createTaskDto)
        {
            try
            {
                return Ok(await _taskRepository.CreateTask(createTaskDto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding task data to the database");
            }

        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userTask"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            var task = _taskRepository.GetTask(id);
            if (task.Result == null)
            {
                return NotFound($"Task with Id - {id} not found in database");
            }
            else
            {
                try
                {
                    var taskResult = await _taskRepository.UpdateTask(id, updateTaskDto);
                    return Ok(taskResult);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating task in database");
                }
            }
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTask>> DeleteTask(int id)
        {
            try
            {
                await _taskRepository.DeleteTask(id);
                return Ok("Tast Data has been deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Tasks with Id - {id} not found");
            }
        }
        #endregion
    }
}
