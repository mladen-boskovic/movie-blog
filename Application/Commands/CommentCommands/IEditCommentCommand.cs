using Application.DTO.InsertUpdateDTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.CommentCommands
{
	public interface IEditCommentCommand : ICommand<InsertUpdateCommentDto>
	{
	}
}
