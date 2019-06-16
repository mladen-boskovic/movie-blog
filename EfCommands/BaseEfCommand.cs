using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
	public abstract class BaseEfCommand
	{
		protected MovieBlogContext Context { get; }

		protected BaseEfCommand(MovieBlogContext context)
		{
			Context = context;
		}
	}
}
