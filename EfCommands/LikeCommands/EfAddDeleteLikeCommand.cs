using Application.Commands.LikeCommands;
using Application.DTO.InsertDeleteDTO;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.LikeCommands
{
	public class EfAddDeleteLikeCommand : BaseEfCommand, IAddDeleteLikeCommand
	{
		public EfAddDeleteLikeCommand(MovieBlogContext context) : base(context)
		{
		}

		public void Execute(InsertDeleteLikeDto request)
		{
			if (!Context.Users.Any(u => u.Id == request.UserId))
				throw new EntityNotFoundException("User");

			if (!Context.Movies.Any(m => m.Id == request.MovieId))
				throw new EntityNotFoundException("Movie");

			var user = Context.Users.Include(u => u.Likes).ThenInclude(l => l.Movie)
				.Where(u => u.Id == request.UserId).FirstOrDefault();

			var like = user.Likes.Where(l => (l.UserId == request.UserId && l.MovieId == request.MovieId))
				.Select(l => new Like
				{
					UserId = l.UserId,
					MovieId = l.MovieId
				}).FirstOrDefault();

			if(like == null)
			{
				user.Likes.Add(new Like
				{
					UserId = request.UserId,
					MovieId = request.MovieId
				});
			}
			else
			{
                user.Likes.Remove(like);        // NE BRISE IZ LISTE NA OVAJ NACIN
                var removeLike = user.Likes.Where(l => (l.UserId == request.UserId && l.MovieId == request.MovieId)).FirstOrDefault();
				user.Likes.Remove(removeLike);
			}
			Context.SaveChanges();
		}
	}
}
