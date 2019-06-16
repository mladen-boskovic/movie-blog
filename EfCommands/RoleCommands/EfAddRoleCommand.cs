using Application.Commands.RoleCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.RoleCommands
{
	public class EfAddRoleCommand : BaseEfCommand, IAddRoleCommand
	{
		public EfAddRoleCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateRoleDto request)
		{
			if (Context.Roles.Any(r => r.Name == request.Name))
				throw new EntityAlreadyExistsException("Role");

			Context.Roles.Add(new Role
			{
				Name = request.Name
			});

			Context.SaveChanges();
		}
	}
}
