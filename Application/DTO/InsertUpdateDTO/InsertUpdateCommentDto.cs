using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO.InsertUpdateDTO
{
	public class InsertUpdateCommentDto
	{
		[Required(ErrorMessage = "Id is required.")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Text is required.")]
		[StringLength(300, MinimumLength = 1, ErrorMessage = "Text must contain 1-300 characters.")]
		public string Text { get; set; }

		[Required(ErrorMessage = "UserId is required.")]
		public int UserId { get; set; }

		[Required(ErrorMessage = "MovieId is required.")]
		public int MovieId { get; set; }
	}
}
