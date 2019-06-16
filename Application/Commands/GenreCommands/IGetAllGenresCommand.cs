using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.GenreCommands
{
	public interface IGetAllGenresCommand : IGetAllCommand<IEnumerable<GenreDto>>
	{
	}
}
