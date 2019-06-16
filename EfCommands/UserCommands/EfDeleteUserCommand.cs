using Application.Commands.UserCommands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.UserCommands
{
	public class EfDeleteUserCommand : BaseEfCommand, IDeleteUserCommand
	{
		public EfDeleteUserCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(int request)
		{
			var user = Context.Users.Find(request);

			if (user == null)
				throw new EntityNotFoundException("User");

			if (user.IsDeleted)
				throw new EntityAlreadyDeletedException("User");

			user.IsDeleted = true;

			Context.SaveChanges();
		}
	}
}
