using Application.Commands.RoleCommands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.RoleCommands
{
	public class EfDeleteRoleCommand : BaseEfCommand, IDeleteRoleCommand
	{
		public EfDeleteRoleCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(int request)
		{
			var role = Context.Roles.Find(request);

			if (role == null)
				throw new EntityNotFoundException("Role");

			if (role.IsDeleted)
				throw new EntityAlreadyDeletedException("Role");

			role.IsDeleted = true;

			Context.SaveChanges();
		}
	}
}
