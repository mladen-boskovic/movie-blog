using Application.Commands.UserCommands;
using Application.DTO;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
	public class EfGetAllUsersCommand : BaseEfCommand, IGetAllUsersCommand
	{
		public EfGetAllUsersCommand(MovieBlogContext context) : base(context)
		{
		}

		public IEnumerable<UserDto> Execute()
		{
			return Context.Users.Where(u => u.IsDeleted == false).Select(u => new UserDto
			{
				Email = u.Email,
				FirstName = u.FirstName,
				Id = u.Id,
				LastName = u.LastName,
				RoleId = u.RoleId,
				Username = u.Username
			});
		}
	}
}
