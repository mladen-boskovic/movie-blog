using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using Application.Commands.ImageCommands;
using Application.Commands.MovieCommands;
using Application.DTO;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Helpers;
using Application.Responses;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
		private readonly IGetMoviesCommand getMovies;
		private readonly IGetMovieCommand getMovie;
		private readonly IAddMovieCommand addMovie;
		private readonly IEditMovieCommand editMovie;
		private readonly IDeleteMovieCommand deleteMovie;

		private readonly IAddImageCommand addImage;

		public MoviesController(IGetMoviesCommand getMovies, IGetMovieCommand getMovie, IAddMovieCommand addMovie, IEditMovieCommand editMovie, IDeleteMovieCommand deleteMovie, IAddImageCommand addImage)
		{
			this.getMovies = getMovies;
			this.getMovie = getMovie;
			this.addMovie = addMovie;
			this.editMovie = editMovie;
			this.deleteMovie = deleteMovie;
			this.addImage = addImage;
		}

        // GET: api/Movies
        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "Keyword": "Harry Potter",
        ///        "MinYear": 2010,
        ///        "MaxYear": 2015,
        ///        "MinLengthMinutes": 60,
        ///        "MaxLengthMinutes": 85,
        ///        "UserId": 1,
        ///        "GenreId": [1, 2]
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Gets all movies</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet]
        public ActionResult<PagedResponse<MovieDto>> Get([FromQuery] MovieSearch search)
        {
			try
			{
				var movies = getMovies.Execute(search);
				return Ok(movies);
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

        // GET: api/Movies/5
        /// <summary>
        /// Gets one movie by ID.
        /// </summary>
        /// <response code="200">Gets one movie by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="500">If server error occurred</response>
        [HttpGet("{id}")]
        public ActionResult<MovieDto> Get(int id)
        {
			try
			{
				var movie = getMovie.Execute(id);
				return Ok(movie);
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

        // POST: api/Movies
        /// <summary>
        /// Adds new movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Name": "Harry Potter",
        ///        "Description": "This movie is about wizards and ...",
        ///        "Year" : 2010,
        ///        "LengthMinutes" : 85,
        ///        "TrailerUrl" : "https://youtube.com/..."
        ///        "UserId" : 1,
        ///        "GenreList" : [1, 2],
        ///        "Image" : image file
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Adds new movie</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If item already exists or two same genres are selected</response>
        /// <response code="422">If image extension is not allowed</response>
        /// <response code="500">If server error occurred</response>
        [HttpPost]
        public ActionResult Post([FromForm] MovieImageDto dto)
        {
			var extension = Path.GetExtension(dto.Image.FileName);

			if (!FileUpload.AllowedExtensions.Contains(extension))
				return UnprocessableEntity("Image extension is not allowed.");

			try
			{
				var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;

				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

				dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

				List<int> genreList = new List<int>();
				genreList = dto.GenreList.ToList();

				var imageId = addImage.Execute(newFileName);

				var movieDto = new InsertUpdateMovieDto
				{
					Description = dto.Description,
					LengthMinutes = dto.LengthMinutes,
					Name = dto.Name,
					TrailerUrl = dto.TrailerUrl,
					UserId = dto.UserId,
					Year = dto.Year,
					ImageId = imageId,
					GenreList = genreList
				};

				try
				{
					addMovie.Execute(movieDto);
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
				catch (ArgumentException e)
				{
					return Conflict(e.Message);
				}
				catch (Exception)
				{
					return StatusCode(500, "An error has occurred.");
				}

			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // PUT: api/Movies/5
        /// <summary>
        /// Edits movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Name": "Harry Potter",
        ///        "Description": "This movie is about wizards and ...",
        ///        "Year" : 2010,
        ///        "LengthMinutes" : 85,
        ///        "TrailerUrl" : "https://youtube.com/..."
        ///        "UserId" : 1,
        ///        "GenreList" : [1, 2],
        ///        "Image" : image file
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Edits movie</response>
        /// <response code="404">If some of the items don't exist</response>
        /// <response code="409">If item already exists or two same genres are selected</response>
        /// <response code="422">If image extension is not allowed</response>
        /// <response code="500">If server error occurred</response>
        [HttpPut("{id}")]
		public ActionResult Put(int id, [FromForm] MovieImageDto dto)
        {
			var extension = Path.GetExtension(dto.Image.FileName);

			if (!FileUpload.AllowedExtensions.Contains(extension))
				return UnprocessableEntity("Image extension is not allowed.");

			try
			{
				var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;

				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

				dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

				List<int> genreList = new List<int>();
				genreList = dto.GenreList.ToList();

				var imageId = addImage.Execute(newFileName);

				var movieDto = new InsertUpdateMovieDto
				{
					Id = id,
					Description = dto.Description,
					LengthMinutes = dto.LengthMinutes,
					Name = dto.Name,
					TrailerUrl = dto.TrailerUrl,
					UserId = dto.UserId,
					Year = dto.Year,
					ImageId = imageId,
					GenreList = genreList
				};

				try
				{
					editMovie.Execute(movieDto);
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
				catch (ArgumentException e)
				{
					return Conflict(e.Message);
				}
				catch (Exception)
				{
					return StatusCode(500, "An error has occurred.");
				}

			}
			catch (Exception)
			{
				return StatusCode(500, "An error has occurred.");
			}
		}

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes one movie by ID.
        /// </summary>
        /// <response code="204">Deletes one movie by ID</response>
        /// <response code="404">If item doesn't exist</response>
        /// <response code="409">If item is already deleted</response>
        /// <response code="500">If server error occurred</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
			try
			{
				deleteMovie.Execute(id);
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
