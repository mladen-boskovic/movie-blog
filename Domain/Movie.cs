using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public class Movie : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Year { get; set; }
		public int LengthMinutes { get; set; }
		public string TrailerUrl { get; set; }
		public int UserId { get; set; }
		public int ImageId { get; set; }

		public User User { get; set; }
		public Image Image { get; set; }
		public ICollection<MovieGenre> MovieGenres { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<Like> Likes { get; set; }
	}
}
