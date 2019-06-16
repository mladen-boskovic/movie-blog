using Application.Commands.CommentCommands;
using Application.DTO;
using Application.Exceptions;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentCommands
{
	public class EfGetCommentsCommand : BaseEfCommand, IGetCommentsCommand
	{
		public EfGetCommentsCommand(MovieBlogContext context) : base(context)
		{
		}

		public PagedResponse<CommentDto> Execute(CommentSearch request)
		{
			var query = Context.Comments.AsQueryable();

			if (request.UserId.HasValue)
			{
				if (!Context.Users.Any(u => u.Id == request.UserId))
					throw new EntityNotFoundException("User");
				query = query.Where(c => c.UserId == request.UserId);
			}

			if (request.MovieId.HasValue)
			{
				if (!Context.Movies.Any(m => m.Id == request.MovieId))
					throw new EntityNotFoundException("Movie");
				query = query.Where(c => c.MovieId == request.MovieId);
			}

			query = query.Where(c => c.IsDeleted == false);

			var totalCount = query.Count();
			var pagesCount = (int)Math.Ceiling((double)(totalCount / request.PerPage));

			query = query.Skip(request.PerPage * (request.CurrentPage - 1)).Take(request.PerPage);

			return new PagedResponse<CommentDto>
			{
				CurrentPage = request.CurrentPage,
				PagesCount = pagesCount,
				PerPage = request.PerPage,
				TotalCount = totalCount,
				Data = query.Select(c => new CommentDto
				{
					Id = c.Id,
					MovieId = c.MovieId,
					UserId = c.UserId,
					Text = c.Text
				})
			};
		}
	}
}
