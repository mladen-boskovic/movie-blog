using Application.DTO;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.RoleCommands
{
	public interface IGetRolesCommand : ICommand<RoleSearch, PagedResponse<RoleDto>>
	{
	}
}
