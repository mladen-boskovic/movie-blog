using Application.Commands.CommentCommands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CommentCommands
{
	public class EfDeleteCommentCommand : BaseEfCommand, IDeleteCommentCommand
	{
		public EfDeleteCommentCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(int request)
		{
			var comment = Context.Comments.Find(request);

			if (comment == null)
				throw new EntityNotFoundException("Comment");

			if (comment.IsDeleted)
				throw new EntityAlreadyDeletedException("Comment");

			comment.IsDeleted = true;

			Context.SaveChanges();
		}
	}
}
