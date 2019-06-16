using Application.Commands.GenreCommands;
using Application.DTO;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.GenreCommands
{
	public class EfGetGenresCommand : BaseEfCommand, IGetGenresCommand
	{
		public EfGetGenresCommand(MovieBlogContext context) : base(context)
		{
		}

		public PagedResponse<GenreDto> Execute(GenreSearch request)
		{
			var query = Context.Genres.AsQueryable();

			if (request.Name != null)
				query = query.Where(g => g.Name.ToLower().Contains(request.Name.Trim().ToLower()));

			query = query.Where(g => g.IsDeleted == false);

			var totalCount = query.Count();
            var pagesCount = (int)Math.Ceiling((double)(totalCount / request.PerPage));

            query = query.Skip(request.PerPage * (request.CurrentPage - 1)).Take(request.PerPage);

			return new PagedResponse<GenreDto>
			{
				CurrentPage = request.CurrentPage,
				PagesCount = pagesCount,
				PerPage = request.PerPage,
				TotalCount = totalCount,
				Data = query.Select(g => new GenreDto
				{
					Id = g.Id,
					Name = g.Name
				})
			};
		}
	}
}
