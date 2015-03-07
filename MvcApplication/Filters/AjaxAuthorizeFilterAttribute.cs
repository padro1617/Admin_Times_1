using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AjaxAuthorizeFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Error Le = new Error();
            Le.IsAuthorized = false;
            int userId = 0;            
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                Int32.TryParse(filterContext.HttpContext.User.Identity.Name, out userId);
            }
            if (userId<=0)
            {
                Le.Type = ErrorType.NoLogin;
                Le.Msg = "当前末登录或者登录超时，请重新登录";
            }
            if (Le.Type != ErrorType.NoError)
            {
                Le.IsAuthorized = true;
                filterContext.Result = new AjaxauthorizedResult(Le);
            }
		}
    }

    /// <summary>
    /// Ajax驗證返回
    /// </summary>
    public class AjaxauthorizedResult : JsonResult
    {
        public AjaxauthorizedResult()
            : this(new Error())
        {

        }
        public AjaxauthorizedResult(Error Le)
        {
            this.Data = Le;
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }
    }

    public enum ErrorType
    {
        /// <summary>
        /// 未出错
        /// </summary>
        NoError = 0,
        /// <summary>
        /// 未登录
        /// </summary>
        NoLogin,
        /// <summary>
        /// 未授权
        /// </summary>
        UnAuthorized,
        /// <summary>
        /// 验证不能过
        /// </summary>
        UnModelValid,
        /// <summary>
        /// 用户自定义
        /// </summary>
        Custom
    }

    public class Error
    {
        /// <summary>
        /// 出错类型
        /// </summary>
        public ErrorType Type { get; set; }
        /// <summary>
        /// 出错信息
        /// </summary>
        public string Msg { get; set; }
        public bool IsAuthorized { get; set; }
    }
}