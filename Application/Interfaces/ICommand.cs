using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
	public interface ICommand<TRequest>
	{
		void Execute(TRequest request);
	}

	public interface ICommand<TRequest, TResponse>
	{
		TResponse Execute(TRequest request);
	}
}
