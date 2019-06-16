using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.MovieCommands
{
	public interface IDeleteMovieCommand : ICommand<int>
	{
	}
}
