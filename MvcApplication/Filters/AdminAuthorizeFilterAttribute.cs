using ShowLibrary.BLL;
using ShowLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShowWeb.Filters
{
    public class AdminAuthorizeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {

            var uInfo = (ShowLibrary.Model.UserInfo)filterContext.HttpContext.Session["ShowWeb5_UserInfo"];
            if (uInfo == null && filterContext.HttpContext.Request.Cookies.Get("M") != null) {
                var arr = Tools.Crypto.Decrypt(filterContext.HttpContext.Request.Cookies.Get("M").Value.ToString()).Split('|');
                var op = Users.Login(0, arr[0], arr[1]);
                if (op.Status == OptStatus.Success) {
                    uInfo = Users.GetInfo((int)op.Flag);
                    FormsAuthentication.SetAuthCookie(uInfo.UserID.ToString(), true);
                    filterContext.HttpContext.Session["ShowWeb5_UserInfo"] = uInfo;
                }
            }
            if (uInfo == null || !uInfo.IsManager) {
                filterContext.Result = new RedirectResult("/Account/ManagerLogin");
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