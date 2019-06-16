using Application.Commands.GenreCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.GenreCommands
{
	public class EfEditGenreCommand : BaseEfCommand, IEditGenreCommand
	{
		public EfEditGenreCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateGenreDto request)
		{
			var genre = Context.Genres.Find(request.Id);

			if (genre == null)
				throw new EntityNotFoundException("Genre");

			if(genre.IsDeleted)
				throw new EntityNotFoundException("Genre");

			if (genre.Name != request.Name)
			{
				if(Context.Genres.Any(g => g.Name == request.Name))
					throw new EntityAlreadyExistsException("Genre with that name");

				genre.UpdatedAt = DateTime.Now;
				genre.Name = request.Name;
				Context.SaveChanges();
			}
		}
	}
}
