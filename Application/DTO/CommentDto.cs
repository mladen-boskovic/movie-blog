using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
	public class CommentDto
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public int UserId { get; set; }
		public int MovieId { get; set; }
	}
}
