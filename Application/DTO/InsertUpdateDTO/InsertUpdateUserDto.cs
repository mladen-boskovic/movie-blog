using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO.InsertUpdateDTO
{
	public class InsertUpdateUserDto
	{
		[Required(ErrorMessage = "Id is required.")]
		public int Id { get; set; }

		[Required(ErrorMessage = "FirstName is required.")]
		//[RegularExpression(@"^[A-ZŠĐŽČĆ][a-zšđžčć]{1-19}$", ErrorMessage = "FirstName must begin with capital letter and must contain 2-20 characters.")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "FirstName must contain 2-20 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is required.")]
		//[RegularExpression(@"^[A-ZŠĐŽČĆ][a-zšđžčć]{1-19}$", ErrorMessage = "LastName must begin with capital letter and must contain 2-20 characters.")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "LastName must contain 2-20 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Username is required.")]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Username must contain 5-20 characters.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Password must contain 5-20 characters.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "RoleId is required.")]
		public int RoleId { get; set; }
	}
}
