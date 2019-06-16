using Application.Commands.MovieCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Helpers;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfCommands.MovieCommands
{
	public class EfAddMovieCommand : BaseEfCommand, IAddMovieCommand
	{
		public EfAddMovieCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateMovieDto request)
		{
			if (!Context.Users.Any(u => u.Id == request.UserId))
				throw new EntityNotFoundException("User");

			if (Context.Movies.Any(m => m.Name.ToLower() == request.Name.ToLower()))
				throw new EntityAlreadyExistsException("Movie");

			if (request.GenreList.Count() != request.GenreList.Distinct().Count())
				throw new ArgumentException("Movie can't have 2 same genres.");

			List<MovieGenre> lista = new List<MovieGenre>();
			foreach(var genreId in request.GenreList)
			{
				if (!Context.Genres.Any(g => g.Id == genreId))
					throw new EntityNotFoundException("Genre");

				lista.Add(new MovieGenre
				{
					MovieId = request.Id,
					GenreId = genreId
				});
			}

			Context.Movies.Add(new Movie
			{
				Name = request.Name,
				Description = request.Description,
				Year = request.Year,
				LengthMinutes = request.LengthMinutes,
				TrailerUrl = request.TrailerUrl,
				UserId = request.UserId,
				MovieGenres = lista,
				ImageId = request.ImageId
			});

			Context.SaveChanges();
		}
	}
}
