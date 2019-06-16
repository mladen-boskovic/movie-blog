using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Role : BaseEntity
	{
		public string Name { get; set; }

		public ICollection<User> Users { get; set; }
	}
}
