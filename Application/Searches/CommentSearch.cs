using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
	public class CommentSearch : BaseSearch
	{
		public int? UserId { get; set; }
		public int? MovieId { get; set; }
	}
}
