using Application.Commands.MovieCommands;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.MovieCommands
{
	public class EfDeleteMovieCommand : BaseEfCommand, IDeleteMovieCommand
	{
		public EfDeleteMovieCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(int request)
		{
			var movie = Context.Movies.Find(request);

			if(movie == null)
				throw new EntityNotFoundException("Movie");

			if (movie.IsDeleted)
				throw new EntityAlreadyDeletedException("Movie");

			movie.IsDeleted = true;

			Context.SaveChanges();
		}
	}
}
