using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.API.Repository;
using TaskMangement.Models;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region CRUDOpration (User)

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return Ok(await _userRepository.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving all User data from the database");
            }
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTask>> GetUser(int id)
        {
            var userResult = await _userRepository.GetUser(id);
            if (userResult != null)
            {
                return Ok(userResult);
            }
            else
            {
                return NotFound($"User with Id - {id} not found in database");
            }
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                await _userRepository.CreateUser(user);
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding user data to the database");
            }

        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            var userdata = _userRepository.GetUser(id);
            if (userdata.Result == null)
            {
                return NotFound($"Task with Id - {id} not found in database");
            }
            else
            {
                try
                {
                    var userResult = await _userRepository.UpdateUser(id, user);
                    return Ok(userResult);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating User in database");
                }
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return Ok("User Data has been deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"User with Id - {id} not found");
            }
        }
        #endregion
    }
}

