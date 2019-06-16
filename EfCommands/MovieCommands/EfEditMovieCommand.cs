using Application.Commands.MovieCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.MovieCommands
{
	public class EfEditMovieCommand : BaseEfCommand, IEditMovieCommand
	{
		public EfEditMovieCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateMovieDto request)
		{
			var movie = Context.Movies.Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
				.Include(m => m.Image).Where(m => m.Id == request.Id).FirstOrDefault();

			if (movie == null)
				throw new EntityNotFoundException("Movie");

			if (movie.IsDeleted)
				throw new EntityNotFoundException("Movie");

			if (request.GenreList.Count() != request.GenreList.Distinct().Count())
				throw new ArgumentException("Movie can't have 2 same genres.");

			if (!Context.Users.Any(u => u.Id == request.UserId))
				throw new EntityNotFoundException("User");

			List<MovieGenre> lista = new List<MovieGenre>();
			foreach (var genreId in request.GenreList)
			{
				if (!Context.Genres.Any(g => g.Id == genreId))
					throw new EntityNotFoundException("Genre");

				lista.Add(new MovieGenre
				{
					MovieId = request.Id,
					GenreId = genreId
				});
			}

			if (movie.Name != request.Name)
			{
				if (Context.Movies.Any(m => m.Name.ToLower() == request.Name.ToLower()))
					throw new EntityAlreadyExistsException("Movie with that name");

				movie.Name = request.Name;
			}

			movie.UpdatedAt = DateTime.Now;
			movie.Description = request.Description;
			movie.Year = request.Year;
			movie.LengthMinutes = request.LengthMinutes;
			movie.TrailerUrl = request.TrailerUrl;
			movie.UserId = request.UserId;
			movie.MovieGenres = lista;
			movie.ImageId = request.ImageId;

			Context.SaveChanges();
		}
	}
}
