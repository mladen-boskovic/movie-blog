using Application.Commands.RoleCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.RoleCommands
{
	public class EfEditRoleCommand : BaseEfCommand, IEditRoleCommand
	{
		public EfEditRoleCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateRoleDto request)
		{
			var role = Context.Roles.Find(request.Id);

			if (role == null)
				throw new EntityNotFoundException("Role");

			if (role.IsDeleted)
				throw new EntityNotFoundException("Role");

			if (role.Name != request.Name)
			{
				if (Context.Roles.Any(r => r.Name == request.Name))
					throw new EntityAlreadyExistsException("Role with that name");

				role.UpdatedAt = DateTime.Now;
				role.Name = request.Name;
				Context.SaveChanges();
			}
		}
	}
}
