using System;

namespace FinalPoint.Web.Business.Interfaces
{
	public interface IHttpFacade
	{
		bool AddToHttpContext(string key, string value);

		string GetFromHttpContext(string key);
	}
}

