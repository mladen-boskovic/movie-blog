using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
	public class UserSearch : BaseSearch
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public int? RoleId { get; set; }
	}
}
