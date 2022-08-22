using Ecommerce_TVHB.ContactDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_TVHB.Controllers
{
    public class PostController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Post
        public ActionResult Index()
        {
            var LstPost = objWebbanhangEntities.Posts.ToList();
            return View(LstPost);
        }
    }
}