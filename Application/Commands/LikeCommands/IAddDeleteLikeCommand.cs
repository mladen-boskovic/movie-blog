using Application.DTO.InsertDeleteDTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.LikeCommands
{
	public interface IAddDeleteLikeCommand : ICommand<InsertDeleteLikeDto>
	{
	}
}
