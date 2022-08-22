using Ecommerce_TVHB.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_TVHB.Controllers
{
    public class ProductController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebbanhangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }

    }
}