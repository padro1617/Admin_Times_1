using AdminLibrary.BLL;
using AdminLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

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
            //if (Session["ShowWeb5_UserInfo"] == null && !string.IsNullOrEmpty(User.Identity.Name)) {
            //    Session["ShowWeb5_UserInfo"] = ShowLibrary.BLL.Users.GetInfo(Convert.ToInt32(User.Identity.Name));
            //}
            //var uInfo = (UserInfo)Session["ShowWeb5_UserInfo"];
            //if (uInfo != null) {
            //    ViewBag.IsLogin = true;
            //}
            //if (uInfo != null && uInfo.IsManager) {
            //    ViewBag.IsManager = true;
            //}
            //else {
            //    ViewBag.IsManager = false;
            //}

            //if (Session["settting-" + TradeID + ""] == null) {
            //    Session["settting-" + TradeID + ""] = SettingList = Setting.Select(TradeID);
            //}
            //else {
            //    SettingList = Session["settting-" + TradeID + ""] as IList<SettingInfo>;
            //}
            //ViewBag.SettingList = SettingList;

            //ViewBag.IsManager = Convert.ToBoolean( Session["IsManager"] == null ? "false" : Session["IsManager"] );

        }

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
