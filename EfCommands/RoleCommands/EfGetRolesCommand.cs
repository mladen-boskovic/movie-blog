using Application.Commands.RoleCommands;
using Application.DTO;
using Application.Responses;
using Application.Searches;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.RoleCommands
{
	public class EfGetRolesCommand : BaseEfCommand, IGetRolesCommand
	{
		public EfGetRolesCommand(MovieBlogContext context) : base(context)
		{
		}

		public PagedResponse<RoleDto> Execute(RoleSearch request)
		{
			var query = Context.Roles.AsQueryable();

			if (request.Name != null)
				query = query.Where(r => r.Name.ToLower().Contains(request.Name.Trim().ToLower()));

			query = query.Where(r => r.IsDeleted == false);

			var totalCount = query.Count();
            var pagesCount = (int)Math.Ceiling((double)(totalCount / request.PerPage));

            query = query.Skip(request.PerPage * (request.CurrentPage - 1)).Take(request.PerPage);

			return new PagedResponse<RoleDto>
			{
				CurrentPage = request.CurrentPage,
				PagesCount = pagesCount,
				PerPage = request.PerPage,
				TotalCount = totalCount,
				Data = query.Select(r => new RoleDto
				{
					Id = r.Id,
					Name = r.Name
				})
			};
		}
	}
}
