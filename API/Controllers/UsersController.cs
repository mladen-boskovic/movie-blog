using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Application.Auth;
using Application.Commands.UserCommands;
using Application.DTO;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private readonly IGetUsersCommand getUsers;
		private readonly IGetUserCommand getUser;
		private readonly IAddUserCommand addUser;
		private readonly IEditUserCommand editUser;
		private readonly IDeleteUserCommand deleteUser;

		public UsersController(IGetUsersCommand getUsers, IGetUserCommand getUser, IAddUserCommand addUser, IEditUserCommand editUser, IDeleteUserCommand deleteUser)
		{
			this.getUsers = getUsers;
			this.getUser = getUser;
			this.addUser = addUser;
			this.editUser = editUser;
			this.deleteUser = deleteUser;
		}

        // GET: api/Users
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "FirstName": "Pera",
        ///        "LastName": "Peric",
        ///        "Email": "pera@gmailc.com",
        ///        "Username": "pera123",
        ///        "RoleId": 1
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Gets all users</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<PagedResponse<UserDto>> Get([FromQuery] UserSearch search)
        {
			try
			{
				var users = getUsers.Execute(search);
				return Ok(users);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // GET: api/Users/5
        /// <summary>
        /// Gets one user by ID.
        /// </summary>
        /// <response code="200">Gets one user by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
			try
			{
				var user = getUser.Execute(id);
				return Ok(user);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // POST: api/Users
        /// <summary>
        /// Adds new user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "FirstName": "Pera",
        ///        "LastName": "Peric",
        ///        "Email" : "pera@gmail.com",
        ///        "Username" : "pera123",
        ///        "Password" : "perapass"
        ///        "RoleId" : 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Adds new user</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If item already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromBody] InsertUpdateUserDto dto)
        {
			try
			{
				addUser.Execute(dto);
				return StatusCode(201);
			}
			catch (EntityAlreadyExistsException e)
			{
				return Conflict(e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
        }

        // PUT: api/Users/5
        /// <summary>
        /// Edits user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "FirstName": "Pera",
        ///        "LastName": "Peric",
        ///        "Email" : "pera@gmail.com",
        ///        "Username" : "pera123",
        ///        "Password" : "perapass"
        ///        "RoleId" : 1
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Edits user</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If item already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] InsertUpdateUserDto dto)
        {
			dto.Id = id;

			try
			{
				editUser.Execute(dto);
				return NoContent();
			}
			catch (EntityAlreadyExistsException e)
			{
				return Conflict(e.Message);
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes one user by ID.
        /// </summary>
        /// <response code="204">Deletes one user by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="409">If item is already deleted</response>
        /// <response code="500">If server error occurred</response>
		[LoggedIn("Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
			try
			{
				deleteUser.Execute(id);
				return NoContent();
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (EntityAlreadyDeletedException e)
			{
				return Conflict(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}
    }
}
