using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Application.Auth;
using Application.Commands.UserCommands;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly Encryption enctiption;
		private readonly ICheckUsersCredentials checkUsersCredentials;

		public AuthController(Encryption enctiption, ICheckUsersCredentials checkUsersCredentials)
		{
			this.enctiption = enctiption;
			this.checkUsersCredentials = checkUsersCredentials;
		}

        // POST: api/Auth
        /// <summary>
        /// Generates key for user who tries to log in if user exists.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Username": "pera",
        ///        "Password": "perapass"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Generates key for user who tries to log in if user exists</response>
        /// <response code="404">If user doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
		public ActionResult Post([FromBody] UsersCredentials uc)
		{
			try
			{
				var user = checkUsersCredentials.Execute(uc);
				var stringObjekat = JsonConvert.SerializeObject(user);
				var encrypted = enctiption.EncryptString(stringObjekat);

				return Ok(new { token = encrypted });
			}
			catch (EntityNotFoundException e)
			{
				return NotFound(e.Message);
			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occured.");
			}
		}
	}
}
