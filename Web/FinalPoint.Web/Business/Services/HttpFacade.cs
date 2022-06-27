namespace FinalPoint.Web.Business.Services
{
    using System;

    using FinalPoint.Web.Business.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class HttpFacade : IHttpFacade
	{
        private readonly IHttpContextAccessor httpContext;

        public HttpFacade(IHttpContextAccessor httpContext)
		{
            this.httpContext = httpContext;
        }

		public bool AddToHttpContext(string key, string value)
        {
			this.httpContext.HttpContext.Session.SetString(key, value);

			return true;
		}

		public string GetFromHttpContext(string key)
		{
			return this.httpContext.HttpContext.Session.GetString(key);
		}
	}
}