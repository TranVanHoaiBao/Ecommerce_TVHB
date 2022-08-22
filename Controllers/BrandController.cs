using Ecommerce_TVHB.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_TVHB.Controllers
{
    public class BrandController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Brand
        public ActionResult ProductBrands(int Id)
        {
            var listProduct = objWebbanhangEntities.Products.Where(n => n.Bandld == Id).ToList();
            return View(listProduct);
        }

    }
}