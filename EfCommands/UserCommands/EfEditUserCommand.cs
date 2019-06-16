using Application.Commands.UserCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
	public class EfEditUserCommand : BaseEfCommand, IEditUserCommand
	{
		public EfEditUserCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateUserDto request)
		{
			var user = Context.Users.Find(request.Id);

			if (user == null)
				throw new EntityNotFoundException("User");

			if (user.IsDeleted)
				throw new EntityNotFoundException("User");

			if (!Context.Roles.Any(r => r.Id == request.RoleId))
				throw new EntityNotFoundException("Role");

			if(user.Email != request.Email)
			{
				if (Context.Users.Any(u => u.Email == request.Email))
					throw new EntityAlreadyExistsException("User with that email");
				user.Email = request.Email;
			}

			if (user.Username != request.Username)
			{
				if (Context.Users.Any(u => u.Username == request.Username))
					throw new EntityAlreadyExistsException("User with that username");
				user.Username = request.Username;
			}

			user.UpdatedAt = DateTime.Now;
			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.Password = request.Password;
			user.RoleId = request.RoleId;

			Context.SaveChanges();
		}
	}
}
