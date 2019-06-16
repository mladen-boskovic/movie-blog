using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}
