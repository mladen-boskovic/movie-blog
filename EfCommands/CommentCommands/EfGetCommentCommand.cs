using Application.Commands.CommentCommands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CommentCommands
{
	public class EfGetCommentCommand : BaseEfCommand, IGetCommentCommand
	{
		public EfGetCommentCommand(MovieBlogContext context) : base(context)
		{
		}

		public CommentDto Execute(int request)
		{
			var comment = Context.Comments.Find(request);

			if (comment == null)
				throw new EntityNotFoundException("Comment");

			if (comment.IsDeleted)
				throw new EntityNotFoundException("Comment");

			return new CommentDto
			{
				Id = comment.Id,
				MovieId = comment.MovieId,
				Text = comment.Text,
				UserId = comment.UserId
			};
		}
	}
}
