using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.GenreCommands;
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
    public class GenresController : ControllerBase
    {
		private readonly IGetGenresCommand getGenres;
		private readonly IGetGenreCommand getGenre;
		private readonly IAddGenreCommand addGenre;
		private readonly IEditGenreCommand editGenre;
		private readonly IDeleteGenreCommand deleteGenre;

		public GenresController(IGetGenresCommand getGenres, IGetGenreCommand getGenre, IAddGenreCommand addGenre, IEditGenreCommand editGenre, IDeleteGenreCommand deleteGenre)
		{
			this.getGenres = getGenres;
			this.getGenre = getGenre;
			this.addGenre = addGenre;
			this.editGenre = editGenre;
			this.deleteGenre = deleteGenre;
		}

        // GET: api/Genres
        /// <summary>
        /// Gets all genres.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Name": "Comedy"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Gets all genres</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<PagedResponse<GenreDto>> Get([FromQuery] GenreSearch search)
        {
			try
			{
				var genres = getGenres.Execute(search);
				return Ok(genres);
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

        // GET: api/Genres/5
        /// <summary>
        /// Gets one genre by ID.
        /// </summary>
        /// <response code="200">Gets one genre by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult<GenreDto> Get(int id)
        {
			try
			{
				var genre = getGenre.Execute(id);
				return Ok(genre);
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

        // POST: api/Genres
        /// <summary>
        /// Adds new genre.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Name": "Comedy"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Adds new comment</response>
        /// <response code="409">If item already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromBody] InsertUpdateGenreDto dto)
        {
			try
			{
				addGenre.Execute(dto);
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

        // PUT: api/Genres/5
        /// <summary>
        /// Edits genre.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Name" : "Comedy"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Edits genre</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If item already exists</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] InsertUpdateGenreDto dto)
        {
			dto.Id = id;

			try
			{
				editGenre.Execute(dto);
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
        /// Deletes one genre by ID.
        /// </summary>
        /// <response code="204">Deletes one genre by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="409">If item is already deleted</response>
        /// <response code="500">If server error occurred</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
			try
			{
				deleteGenre.Execute(id);
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
