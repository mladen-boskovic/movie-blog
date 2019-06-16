using Application.Commands.RoleCommands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.RoleCommands
{
	public class EfGetRoleCommand : BaseEfCommand, IGetRoleCommand
	{
		public EfGetRoleCommand(MovieBlogContext context) : base(context)
		{
		}

		public RoleDto Execute(int request)
		{
			var role = Context.Roles.Find(request);

			if (role == null)
				throw new EntityNotFoundException("Role");

			if (role.IsDeleted)
				throw new EntityNotFoundException("Role");

			return new RoleDto
			{
				Id = role.Id,
				Name = role.Name
			};
		}
	}
}
