using Application.Commands.MovieCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.MovieCommands
{
	public class EfGetMoviesCommand : BaseEfCommand, IGetMoviesCommand
	{
		public EfGetMoviesCommand(MovieBlogContext context) : base(context)
		{
		}

		public PagedResponse<MovieDto> Execute(MovieSearch request)
		{
			var query = Context.Movies.Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre).AsQueryable();

			if (request.UserId.HasValue)
			{
				if (!Context.Users.Any(u => u.Id == request.UserId))
					throw new EntityNotFoundException("User");

				query = query.Where(m => m.UserId == request.UserId);
			}

			if (request.GenreId.HasValue)
			{
				if (!Context.Genres.Any(g => g.Id == request.GenreId))
					throw new EntityNotFoundException("Genre");

				query = query.Where(m => m.MovieGenres.Any(mg => mg.GenreId == request.GenreId));
			}

			if (request.Keyword != null)
				query = query.Where(m => m.Name.ToLower().Contains(request.Keyword.Trim().ToLower()));

			if (request.MinYear.HasValue)
				query = query.Where(m => m.Year >= request.MinYear);

			if (request.MaxYear.HasValue)
				query = query.Where(m => m.Year <= request.MaxYear);

			if (request.MinLengthMinutes.HasValue)
				query = query.Where(m => m.LengthMinutes >= request.MinLengthMinutes);

			if (request.MaxLengthMinutes.HasValue)
				query = query.Where(m => m.LengthMinutes <= request.MaxLengthMinutes);

			query = query.Where(m => m.IsDeleted == false);

			var totalCount = query.Count();
            var pagesCount = (int)Math.Ceiling((double)(totalCount / request.PerPage));

            query = query.Skip(request.PerPage * (request.CurrentPage - 1)).Take(request.PerPage);

			var movies = new PagedResponse<MovieDto>
			{
				CurrentPage = request.CurrentPage,
				PagesCount = pagesCount,
				TotalCount = totalCount,
				PerPage = request.PerPage,
				Data = query.Select(m => new MovieDto
				{
					Id = m.Id,
					Description = m.Description,
					LengthMinutes = m.LengthMinutes,
					Name = m.Name,
					TrailerUrl = m.TrailerUrl,
					Year = m.Year,
					UserId = m.User.Id,
					User = m.User.Username,
					Image = m.Image.FileName,
					CreatedAt = m.CreatedAt,
                    GenreList = m.MovieGenres.Select(mg => mg.GenreId)
				})
			};

			return movies;
		}
	}
}
