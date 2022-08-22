using Ecommerce_TVHB.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_TVHB.Controllers
{
    public class CategoryController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objWebbanhangEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objWebbanhangEntities.Products.Where(n => n.Categoryid == Id).ToList();
            return View(listProduct);
        }
    }
}