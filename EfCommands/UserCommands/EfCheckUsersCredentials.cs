using Application.Auth;
using Application.Commands.UserCommands;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
	public class EfCheckUsersCredentials : BaseEfCommand, ICheckUsersCredentials
	{
		public EfCheckUsersCredentials(MovieBlogContext context) : base(context)
		{
		}

		public LoggedUser Execute(UsersCredentials request)
		{
			var user = Context.Users.Include(u => u.Role)
				.Where(u => (u.Username == request.Username && u.Password == request.Password)).FirstOrDefault();

			if (user == null)
				throw new EntityNotFoundException("User with that username and password");

			return new LoggedUser
				{
					FirstName = user.FirstName,
					Id = user.Id,
					//IsLogged = false,
					LastName = user.LastName,
					Role = user.Role.Name,
					Username = user.Username
				};
		}
	}
}
