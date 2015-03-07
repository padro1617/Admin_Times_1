using AdminLibrary.BLL;
using AdminLibrary.Model;
using System;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class BaseController : Controller
    {
        public byte UserID { get; set; }
        public string UserName { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (MvcApplication.isInstall == false) {
                filterContext.Result = new RedirectResult("/Install/Index");
                return;
            }

            //if (info.TradeID == 0) {
            //    filterContext.Result = new HttpNotFoundResult();
            //}
            //else if (info.IsRedirect && info.LocalHostUrl != Request.Url.Host) {
            //    Tools.WebUtils.Page301Redirect("http://" + info.LocalHostUrl + Request.RawUrl);
            //}
            //else {
            //    info.LocalHostUrl = Request.Url.Host;

            //    TradeID = info.TradeID;
            //    TradeName = info.TradeName;
            //    ViewBag.TradeID = TradeID;
            //    ViewBag.Theme = info.Theme;
            //    ViewBag.FirstInterestInfo = Interest.SelectList(TradeID, InterestSelectType.Commend).FirstOrDefault() ?? new InterestInfo();
            //}
			//是否登录
			ViewBag.IsLogin = false;
	        if (Session["times_admin_userinfo"] == null)
	        {
				if (!string.IsNullOrEmpty( User.Identity.Name ) ) {
					var timesadminuserinfo= AdminLibrary.BLL.TimesAdmin.GetInfo( Convert.ToByte( User.Identity.Name ) );
					Session["times_admin_userinfo"] = timesadminuserinfo;
					ViewBag.IsLogin = true;
				}
	        }
	        else
	        {
				var uInfo = (times_admin) Session["times_admin_userinfo"];
				if ( uInfo != null ) {
					ViewBag.IsLogin = true;
				}
			}
	        if (ViewBag.IsLogin)
	        {
				//管理员登录 需要更新最新登录时间和IP
				UpdateLoginIPDelegate UpdateLoginIP = UpdateLoginIPLog;
		        UpdateLoginIP.BeginInvoke(null, null);
	        }

        }


		#region 异步写入登录操作日志

	    private delegate void UpdateLoginIPDelegate();

	    private void UpdateLoginIPLog()
	    {
		}

	    #endregion

		/// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns></returns>
        public int GetUserId() {
            int userId = 0;
            if (Request.IsAuthenticated) {
                Int32.TryParse(User.Identity.Name, out userId);
            }
            return userId;
        }
    }
}
