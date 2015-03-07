using AdminLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApplication.Filters
{
	/// <summary>
	/// 登录过滤器
	/// </summary>
    public class CheckLoginAttribute  : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
			var uInfo = (AdminLibrary.Model.times_admin) filterContext.HttpContext.Session["times_admin_userinfo"];
            if (uInfo == null && filterContext.HttpContext.Request.Cookies.Get("M") != null) {
				var arr = DotNet.Utilities.DESEncrypt.Decrypt( filterContext.HttpContext.Request.Cookies.Get( "M" ).Value.ToString() ).Split( '|' );
                var op = AdminLibrary.BLL.TimesAdmin.LoginCheck(arr[0], arr[1]);
                if (op.Status == OptStatus.Success) {
                    uInfo = Users.GetInfo((int)op.Flag);
                    FormsAuthentication.SetAuthCookie(uInfo.UserID.ToString(), true);
                    filterContext.HttpContext.Session["ShowWeb5_UserInfo"] = uInfo;
                }
            }
            if (uInfo == null || !uInfo.IsManager) {
                filterContext.Result = new RedirectResult("/Account/ManagerLogin");

				// 处理方法
				MvcApplication.ErrorRedirect( filterContext ); 
                return;
            }
            //todo

            //filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.StatusCode = 401;
            //filterContext.HttpContext.Response.End();

            base.OnActionExecuting(filterContext);
        }
    }
	
}