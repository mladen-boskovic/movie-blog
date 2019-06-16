using Application.Commands.MovieCommands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.MovieCommands
{
	public class EfGetMovieCommand : BaseEfCommand, IGetMovieCommand
	{
		public EfGetMovieCommand(MovieBlogContext context) : base(context)
		{
		}

		public MovieDto Execute(int request)
		{
			var query = Context.Movies.AsQueryable();
			var movie = query.Include(m => m.User).Include(m => m.Image)
				.Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre).Where(m => m.Id == request).FirstOrDefault();

			if(movie == null)
				throw new EntityNotFoundException("Movie");

			if (movie.IsDeleted)
				throw new EntityNotFoundException("Movie");

			return new MovieDto
			{
				Id = movie.Id,
				Name = movie.Name,
				Description = movie.Description,
				Year = movie.Year,
				LengthMinutes = movie.LengthMinutes,
				TrailerUrl = movie.TrailerUrl,
				UserId = movie.User.Id,
				User = movie.User.Username,
				Image = movie.Image.FileName,
				CreatedAt = movie.CreatedAt,
				GenreList = movie.MovieGenres.Select(mg => mg.GenreId)
			};
		}
	}
}
