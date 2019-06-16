using Application.Commands.UserCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Interfaces;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserCommands
{
	public class EfAddUserCommand : BaseEfCommand, IAddUserCommand
	{
		private readonly IEmailSender emailSender;

		public EfAddUserCommand(MovieBlogContext context, IEmailSender emailSender) : base(context)
		{
			this.emailSender = emailSender;
		}

		public void Execute(InsertUpdateUserDto request)
		{
			if (!Context.Roles.Any(r => r.Id == request.RoleId))
				throw new EntityNotFoundException("Role");

			if (Context.Users.Any(u => u.Username == request.Username))
				throw new EntityAlreadyExistsException("Username");

			if (Context.Users.Any(u => u.Email == request.Email))
				throw new EntityAlreadyExistsException("Email");

			Context.Users.Add(new User
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Password = request.Password,
				RoleId = request.RoleId,
				Username = request.Username
			});

			Context.SaveChanges();

			emailSender.Subject = "Uspešna registracija";
			emailSender.Body = "Vaš nalog je uspešno napravljen!";
			emailSender.ToEmail = request.Email;
			emailSender.Send();
		}
	}
}
