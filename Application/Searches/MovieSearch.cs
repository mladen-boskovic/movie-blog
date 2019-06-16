using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
	public class MovieSearch : BaseSearch
    {
		public string Keyword { get; set; }
		public int? MinYear { get; set; }
		public int? MaxYear { get; set; }
		public int? MinLengthMinutes { get; set; }
		public int? MaxLengthMinutes { get; set; }
		public int? UserId { get; set; }
		public int? GenreId { get; set; }
	}
}
