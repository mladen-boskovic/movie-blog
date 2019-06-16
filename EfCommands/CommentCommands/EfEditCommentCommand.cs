using Application.Commands.CommentCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentCommands
{
	public class EfEditCommentCommand : BaseEfCommand, IEditCommentCommand
	{
		public EfEditCommentCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateCommentDto request)
		{
			var comment = Context.Comments.Find(request.Id);

			if(comment == null)
				throw new EntityNotFoundException("Comment");

			if (comment.IsDeleted)
				throw new EntityNotFoundException("Comment");

			comment.UpdatedAt = DateTime.Now;
			comment.Text = request.Text;

			Context.SaveChanges();
		}
	}
}
