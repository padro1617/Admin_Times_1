using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
         
        // GET: /Admin/Home/
        public ActionResult Index()
        {           
            return  PartialView();
        }
        //控制中心
        public ActionResult Center()
        {
            return PartialView();
        }
        
        //帮助文档
        public ActionResult Help()
        {
            return PartialView();
        }
    }
}
