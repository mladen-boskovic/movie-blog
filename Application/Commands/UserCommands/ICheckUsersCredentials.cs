using Application.Auth;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.UserCommands
{
	public interface ICheckUsersCredentials : ICommand<UsersCredentials, LoggedUser>
	{
	}
}
