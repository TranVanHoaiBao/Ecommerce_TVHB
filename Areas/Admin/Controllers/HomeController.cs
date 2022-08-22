using Ecommerce_TVHB.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_TVHB.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var lstProduct = objWebbanhangEntities.Products.ToList();
                return View(lstProduct);
            }
            else
            {
                return View("Login");
            }
            //return View();
        }
    }
}