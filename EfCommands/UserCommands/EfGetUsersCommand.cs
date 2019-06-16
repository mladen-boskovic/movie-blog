using Application.Commands.UserCommands;
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

namespace EfCommands.UserCommands
{
	public class EfGetUsersCommand : BaseEfCommand, IGetUsersCommand
	{
		public EfGetUsersCommand(MovieBlogContext context) : base(context)
		{
		}

		public PagedResponse<UserDto> Execute(UserSearch request)
		{
			var query = Context.Users.Include(u => u.Role).AsQueryable();

			if (request.RoleId.HasValue)
			{
				if (!Context.Roles.Any(r => r.Id == request.RoleId))
					throw new EntityNotFoundException("Role");
				query = query.Where(u => u.RoleId == request.RoleId);
			}

			if (request.FirstName != null)
				query = query.Where(u => u.FirstName.ToLower().Contains(request.FirstName.Trim().ToLower()));

			if (request.LastName != null)
				query = query.Where(u => u.LastName.ToLower().Contains(request.LastName.Trim().ToLower()));

			if (request.Email != null)
				query = query.Where(u => u.Email.ToLower().Contains(request.Email.Trim().ToLower()));

			if (request.Username != null)
				query = query.Where(u => u.Username.ToLower().Contains(request.Username.Trim().ToLower()));

			query = query.Where(u => u.IsDeleted == false);

			var totalCount = query.Count();
            var pagesCount = (int)Math.Ceiling((double)(totalCount / request.PerPage));

            query = query.Skip(request.PerPage * (request.CurrentPage - 1)).Take(request.PerPage);

			return new PagedResponse<UserDto>
			{
				CurrentPage = request.CurrentPage,
				PagesCount = pagesCount,
				PerPage = request.PerPage,
				TotalCount = totalCount,
				Data = query.Select(u => new UserDto
				{
					Id = u.Id,
					Email = u.Email,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Username = u.Username,
                    Password = u.Password,
					RoleId = u.RoleId,
                    Role = u.Role.Name,
                    CreatedAt = u.CreatedAt
				})
			};
		}
	}
}
