using Application.Commands.GenreCommands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.GenreCommands
{
	public class EfDeleteGenreCommand : BaseEfCommand, IDeleteGenreCommand
	{
		public EfDeleteGenreCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(int request)
		{
			var genre = Context.Genres.Find(request);

			if (genre == null)
				throw new EntityNotFoundException("Genre");

			if (genre.IsDeleted)
				throw new EntityAlreadyDeletedException("Genre");

			genre.IsDeleted = true;

			Context.SaveChanges();
		}
	}
}
