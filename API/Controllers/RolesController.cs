using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.RoleCommands;
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
    public class RolesController : ControllerBase
    {
		private readonly IGetRolesCommand getRoles;
		private readonly IGetRoleCommand getRole;
		private readonly IAddRoleCommand addRole;
		private readonly IEditRoleCommand editRole;
		private readonly IDeleteRoleCommand deleteRole;

		public RolesController(IGetRolesCommand getRoles, IGetRoleCommand getRole, IAddRoleCommand addRole, IEditRoleCommand editRole, IDeleteRoleCommand deleteRole)
		{
			this.getRoles = getRoles;
			this.getRole = getRole;
			this.addRole = addRole;
			this.editRole = editRole;
			this.deleteRole = deleteRole;
		}

        // GET: api/Roles
        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Name": "User"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Gets all roles</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<PagedResponse<RoleDto>> Get([FromQuery] RoleSearch search)
        {
			try
			{
				var roles = getRoles.Execute(search);
				return Ok(roles);
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

        // GET: api/Roles/5
        /// <summary>
        /// Gets one role by ID.
        /// </summary>
        /// <response code="200">Gets one role by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult<RoleDto> Get(int id)
        {
			try
			{
				var role = getRole.Execute(id);
				return Ok(role);
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

        // POST: api/Roles
        /// <summary>
        /// Adds new role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Name": "User"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Adds new role</response>
        /// <response code="409">If item already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromBody] InsertUpdateRoleDto dto)
        {
			try
			{
				addRole.Execute(dto);
				return StatusCode(201);
			}
			catch (EntityAlreadyExistsException e)
			{
				return Conflict(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // PUT: api/Roles/5
        /// <summary>
        /// Edits role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Name" : "User"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Edits genre</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If item already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] InsertUpdateRoleDto dto)
        {
			dto.Id = id;

			try
			{
				editRole.Execute(dto);
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
        /// Deletes one role by ID.
        /// </summary>
        /// <response code="204">Deletes one role by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="409">If item is already deleted</response>
        /// <response code="500">If server error occurred</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
			try
			{
				deleteRole.Execute(id);
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
