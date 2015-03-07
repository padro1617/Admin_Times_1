using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcApplication
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        
        public static bool isInstall = true;
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }



        protected void Session_Start(object sender, EventArgs e) {

        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            try {
                const string sessionParamName = "ASPSESSID";
                const string sessionCookieName = "ASP.NET_SESSIONID";

                if (HttpContext.Current.Request.Form[sessionParamName] != null) {
                    UpdateCookie(sessionCookieName, HttpContext.Current.Request.Form[sessionParamName]);
                }
                else if (HttpContext.Current.Request.QueryString[sessionParamName] != null) {
                    UpdateCookie(sessionCookieName, HttpContext.Current.Request.QueryString[sessionParamName]);
                }
            }
            catch (Exception) {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Session");
            }

            try {
                const string authParamName = "AUTHID";
                string authCookieName = System.Web.Security.FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[authParamName] != null) {
                    UpdateCookie(authCookieName, HttpContext.Current.Request.Form[authParamName]);
                }
                else if (HttpContext.Current.Request.QueryString[authParamName] != null) {
                    UpdateCookie(authCookieName, HttpContext.Current.Request.QueryString[authParamName]);
                }

            }
            catch (Exception) {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Forms Authentication");
            }
        }

        private static void UpdateCookie(string cookieName, string cookieValue) {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieName);
            if (cookie == null) {
                cookie = new HttpCookie(cookieName);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            cookie.Value = cookieValue;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }


        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }


		internal static void ErrorRedirect( ActionExecutingContext filterContext ) {

			filterContext.Result = new RedirectToRouteResult( "Default", new RouteValueDictionary( new { controller = "Home", action = "Default" } ) );
		}
    }
}