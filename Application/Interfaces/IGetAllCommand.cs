using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
	public interface IGetAllCommand<TResponse>
	{
		TResponse Execute();
	}
}
