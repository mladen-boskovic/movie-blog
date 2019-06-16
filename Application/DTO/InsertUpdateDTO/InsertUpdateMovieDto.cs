using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO.InsertUpdateDTO
{
	public class InsertUpdateMovieDto
	{
		[Required(ErrorMessage = "Id is required.")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		[StringLength(100, MinimumLength = 1, ErrorMessage = "Name must contain 1-100 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is required.")]
		[StringLength(300, MinimumLength = 1, ErrorMessage = "Description must contain 1-300 characters.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Year is required.")]
		[Range(1886, 2030, ErrorMessage = "Year must me between 1886-2030.")]
		public int Year { get; set; }

		[Required(ErrorMessage = "LengthMinutes is required.")]
		[Range(1, 100000, ErrorMessage = "LengthMinutes must me between 1-100000.")]
		public int LengthMinutes { get; set; }

		[Required(ErrorMessage = "TrailerUrl is required.")]
		[StringLength(100, MinimumLength = 10, ErrorMessage = "TrailerUrl must contain 10-100 characters.")]
		[Url(ErrorMessage = "Please enter a valid URL.")]
		public string TrailerUrl { get; set; }

		[Required(ErrorMessage = "UserId is required.")]
		public int UserId { get; set; }

		[Required(ErrorMessage = "GenreList is required.")]
		[MinLength(1, ErrorMessage = "Movie must have at least 1 genre.")]
		public IEnumerable<int> GenreList { get; set; }

		[Required(ErrorMessage = "ImageId is required.")]
		public int ImageId { get; set; }
	}
}
