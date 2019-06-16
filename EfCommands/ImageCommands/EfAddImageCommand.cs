using Application.Commands.ImageCommands;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.ImageCommands
{
	public class EfAddImageCommand : BaseEfCommand, IAddImageCommand
	{
		public EfAddImageCommand(MovieBlogContext context) : base(context)
		{
		}

		public int Execute(string request)
		{
			if (Context.Images.Any(i => i.FileName == request))
				throw new EntityAlreadyExistsException("Image");

			var image = new Image
			{
				FileName = request
			};

			Context.Images.Add(image);
			Context.SaveChanges();

			return image.Id;
		}
	}
}
