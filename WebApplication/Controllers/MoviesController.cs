using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.GenreCommands;
using Application.Commands.ImageCommands;
using Application.Commands.MovieCommands;
using Application.Commands.UserCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Helpers;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO;

namespace WebApplication.Controllers
{
    public class MoviesController : Controller
    {
		private readonly IGetMoviesCommand getMovies;
		private readonly IGetMovieCommand getMovie;
		private readonly IAddMovieCommand addMovie;
		private readonly IEditMovieCommand editMovie;
		private readonly IDeleteMovieCommand deleteMovie;

		private readonly IAddImageCommand addImage;

		private readonly IGetAllUsersCommand getAllUsers;
		private readonly IGetAllGenresCommand getAllGenres;

		public MoviesController(IGetMoviesCommand getMovies, IGetMovieCommand getMovie, IAddMovieCommand addMovie, IEditMovieCommand editMovie, IDeleteMovieCommand deleteMovie, IAddImageCommand addImage, IGetAllUsersCommand getAllUsers, IGetAllGenresCommand getAllGenres)
		{
			this.getMovies = getMovies;
			this.getMovie = getMovie;
			this.addMovie = addMovie;
			this.editMovie = editMovie;
			this.deleteMovie = deleteMovie;
			this.addImage = addImage;
			this.getAllUsers = getAllUsers;
			this.getAllGenres = getAllGenres;
		}

		// GET: Movies
		public ActionResult Index([FromQuery] MovieSearch search)
        {
			try
			{
				var movies = getMovies.Execute(search);
				return View(movies.Data);
			}
			catch (EntityNotFoundException)
			{
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
		}

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
			try
			{
				var movie = getMovie.Execute(id);
				return View(movie);
			}
			catch (EntityNotFoundException)
			{
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
		}

        // GET: Movies/Create
        public ActionResult Create()
        {
			try
			{
				ViewBag.Users = getAllUsers.Execute();
				ViewBag.Genres = getAllGenres.Execute();
				return View();
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieImageDto dto)
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Please enter a valid data.";
				return View(dto);
			}

			var extension = Path.GetExtension(dto.Image.FileName);

			if (!FileUpload.AllowedExtensions.Contains(extension))
			{
				TempData["error"] = "Image extension is not allowed.";
				ViewBag.Users = getAllUsers.Execute();
				ViewBag.Genres = getAllGenres.Execute();
				return View();
			}

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
					TempData["success"] = "Movie successfully added.";
					return RedirectToAction(nameof(Index));
				}
				catch (EntityAlreadyExistsException e)
				{
					TempData["error"] = e.Message;
					ViewBag.Users = getAllUsers.Execute();
					ViewBag.Genres = getAllGenres.Execute();
					return View(dto);
				}
				catch (EntityNotFoundException)
				{
					return RedirectToAction(nameof(Index));
				}
				catch (ArgumentException)
				{
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					TempData["error"] = "An error has occurred.";
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
			try
			{
				var movie = getMovie.Execute(id);

				ViewBag.Users = getAllUsers.Execute();
				ViewBag.Genres = getAllGenres.Execute();

				return View(new MovieImageDto {
					Id = movie.Id,
					Description = movie.Description,
					LengthMinutes = movie.LengthMinutes,
					Name = movie.Name,
					TrailerUrl = movie.TrailerUrl,
					UserId = movie.UserId,
					Year = movie.Year,
					GenreList = movie.GenreList
					
				});
			}
			catch (EntityNotFoundException)
			{
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieImageDto dto)
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Please enter a valid data.";
				return View(dto);
			}

			var extension = Path.GetExtension(dto.Image.FileName);

			if (!FileUpload.AllowedExtensions.Contains(extension))
			{
				TempData["error"] = "Image extension is not allowed.";
				ViewBag.Users = getAllUsers.Execute();
				ViewBag.Genres = getAllGenres.Execute();
				return View();
			}

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
					TempData["success"] = "Movie successfully updated.";
					return RedirectToAction(nameof(Index));
				}
				catch (EntityAlreadyExistsException e)
				{
					TempData["error"] = e.Message;
					ViewBag.Users = getAllUsers.Execute();
					ViewBag.Genres = getAllGenres.Execute();
					return View(dto);
				}
				catch (EntityNotFoundException)
				{
					return RedirectToAction(nameof(Index));
				}
				catch (ArgumentException)
				{
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					TempData["error"] = "An error has occurred.";
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: Movies/Delete/5
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
			try
			{
				deleteMovie.Execute(id);
				TempData["success"] = "Movie successfully deleted.";
				return RedirectToAction(nameof(Index));
			}
			catch (EntityNotFoundException)
			{
				return RedirectToAction(nameof(Index));
			}
			catch (EntityAlreadyDeletedException)
			{
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["error"] = "An error has occurred.";
				return RedirectToAction(nameof(Index));
			}
        }
    }
}