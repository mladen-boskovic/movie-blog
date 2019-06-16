using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Image : BaseEntity
	{
		public string FileName { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
