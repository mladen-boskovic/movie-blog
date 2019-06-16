using Application.Commands.GenreCommands;
using Application.DTO;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.GenreCommands
{
	public class EfGetAllGenresCommand : BaseEfCommand, IGetAllGenresCommand
	{
		public EfGetAllGenresCommand(MovieBlogContext context) : base(context)
		{
		}

		public IEnumerable<GenreDto> Execute()
		{
			return Context.Genres.Where(g => g.IsDeleted == false).Select(g => new GenreDto
			{
				Id = g.Id,
				Name = g.Name
			});
		}
	}
}
