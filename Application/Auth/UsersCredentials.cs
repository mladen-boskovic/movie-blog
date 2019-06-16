using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Auth
{
	public class UsersCredentials
	{
		[Required(ErrorMessage = "Username is required.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }
	}
}
