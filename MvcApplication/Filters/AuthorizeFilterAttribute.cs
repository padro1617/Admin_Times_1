using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Filters
{
	public class AuthorizeFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting( ActionExecutingContext filterContext ) {
			if ( filterContext.HttpContext.Request.IsAuthenticated == false ) {
				filterContext.Result = new RedirectResult( "/Account/Login" );
				return;
			}
			//filterContext.HttpContext.Response.Clear();
			//filterContext.HttpContext.Response.StatusCode = 401;
			//filterContext.HttpContext.Response.End();

			base.OnActionExecuting( filterContext );
		}
	}
}