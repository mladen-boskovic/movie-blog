using Application.Commands.UserCommands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
	public class EfGetUserCommand : BaseEfCommand, IGetUserCommand
	{
		public EfGetUserCommand(MovieBlogContext context) : base(context)
		{
		}

		public UserDto Execute(int request)
		{
            var user = Context.Users.Include(u => u.Role).Where(u => u.Id == request).FirstOrDefault();

			if (user == null)
				throw new EntityNotFoundException("User");

			if (user.IsDeleted)
				throw new EntityNotFoundException("User");

			return new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Username = user.Username,
                Password = user.Password,
				RoleId = user.RoleId,
                Role = user.Role.Name,
                CreatedAt = user.CreatedAt
			};
		}
	}
}
