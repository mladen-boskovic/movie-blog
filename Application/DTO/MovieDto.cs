using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
	public class MovieDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Year { get; set; }
		public int LengthMinutes { get; set; }
		public string TrailerUrl { get; set; }
		public int UserId { get; set; }
		public string User { get; set; }
		public string Image { get; set; }
		public DateTime CreatedAt { get; set; }
		public IEnumerable<int> GenreList { get; set; }
	}
}
