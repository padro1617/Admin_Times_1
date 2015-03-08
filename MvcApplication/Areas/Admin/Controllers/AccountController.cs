using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class AccountController : BaseController
    {
        //用户注册
        public ActionResult Register()
        {
            return View();
        }
        //用户登录
        public ActionResult Login()
        {
            return View();
        }
        //退出
        public ActionResult LogOut()
        {
            return View();
        }
        //密码重设
        public ActionResult ResetPwd()
        {
            return View();
        }

        //
        // GET: /Admin/Account/

        public ActionResult Index()
        {            
            return PartialView();
        }


        public ActionResult Edit()
        {
            return PartialView();
        }

    }
}
