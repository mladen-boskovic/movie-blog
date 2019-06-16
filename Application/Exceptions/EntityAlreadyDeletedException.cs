using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
	public class EntityAlreadyDeletedException : Exception
	{
		public EntityAlreadyDeletedException(string entity) : base($"{entity} is already deleted.")
		{

		}
	}
}
