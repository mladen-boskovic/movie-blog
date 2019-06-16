using Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
	public class LoggedIn : Attribute, IResourceFilter
	{
		private readonly string role;
		public LoggedIn(string role)
		{
			this.role = role;
		}

		public LoggedIn()
		{

		}

		public void OnResourceExecuted(ResourceExecutedContext context)
		{

		}

		public void OnResourceExecuting(ResourceExecutingContext context)
		{
			var user = context.HttpContext.RequestServices.GetService<LoggedUser>();

			if (!user.IsLogged)
			{
				context.Result = new UnauthorizedResult();
			}
			else
			{
				if (role != null)
				{
					if (user.Role != role)
					{
						context.Result = new UnauthorizedResult();
					}
				}
			}
		}
	}
}
