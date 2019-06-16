using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO.InsertUpdateDTO
{
	public class InsertUpdateGenreDto
	{
		[Required(ErrorMessage = "Id is required.")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		[StringLength(20, MinimumLength = 1, ErrorMessage = "Name must contain 1-20 characters.")]
		public string Name { get; set; }
	}
}
