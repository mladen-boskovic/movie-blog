using Application.Commands.GenreCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.GenreCommands
{
	public class EfAddGenreCommand : BaseEfCommand, IAddGenreCommand
	{
		public EfAddGenreCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertUpdateGenreDto request)
		{
			if (Context.Genres.Any(g => g.Name == request.Name))
				throw new EntityAlreadyExistsException("Genre");

			Context.Genres.Add(new Genre
			{
				Name = request.Name
			});

			Context.SaveChanges();
		}
	}
}
