using Application.Commands.GenreCommands;
using Application.DTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.GenreCommands
{
	public class EfGetGenreCommand : BaseEfCommand, IGetGenreCommand
	{
		public EfGetGenreCommand(MovieBlogContext context) : base(context)
		{
		}

		public GenreDto Execute(int request)
		{
			var genre = Context.Genres.Find(request);

			if (genre == null)
				throw new EntityNotFoundException("Genre");

			if (genre.IsDeleted)
				throw new EntityNotFoundException("Genre");

			return new GenreDto
			{
				Id = genre.Id,
				Name = genre.Name
			};
		}
	}
}
