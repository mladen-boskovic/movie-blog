using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO.InsertDeleteDTO
{
	public class InsertDeleteLikeDto
	{
		[Required(ErrorMessage = "UserId is required.")]
		public int UserId { get; set; }

		[Required(ErrorMessage = "MovieId is required.")]
		public int MovieId { get; set; }
	}
}
