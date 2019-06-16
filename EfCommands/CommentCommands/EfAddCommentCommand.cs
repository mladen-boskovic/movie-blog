using Application.Commands.CommentCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentCommands
{
	public class EfAddCommentCommand : BaseEfCommand, IAddCommentCommand
	{
		public EfAddCommentCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateCommentDto request)
		{
			if (!Context.Users.Any(u => u.Id == request.UserId))
				throw new EntityNotFoundException("User");

			if (!Context.Movies.Any(m => m.Id == request.MovieId))
				throw new EntityNotFoundException("Movie");

			Context.Comments.Add(new Comment
			{
				Text = request.Text,
				UserId = request.UserId,
				MovieId = request.MovieId
			});

			Context.SaveChanges();
		}
	}
}
