using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.LikeCommands;
using Application.DTO.InsertDeleteDTO;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
		private readonly IAddDeleteLikeCommand addDeleteLike;

		public LikesController(IAddDeleteLikeCommand addDeleteLike)
		{
			this.addDeleteLike = addDeleteLike;
		}

        // POST: api/Likes
        /// <summary>
        /// Adds or deletes like.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "UserId": 1,
        ///        "MovieId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Adds or deletes like</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
		public ActionResult InsertDelete([FromBody] InsertDeleteLikeDto dto)
		{
			try
			{
				addDeleteLike.Execute(dto);
				return NoContent();
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
	}
}
