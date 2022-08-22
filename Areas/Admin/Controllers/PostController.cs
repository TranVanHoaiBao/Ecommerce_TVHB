using Ecommerce_TVHB.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_TVHB.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Admin/Post
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstPost = new List<Post>();

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                lstPost = objWebbanhangEntities.Posts.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstPost = objWebbanhangEntities.Posts.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstPost.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Post objpost)
        {
            try
            {
                if (objpost.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objpost.ImageUpload.FileName);
                    String extension = Path.GetExtension(objpost.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objpost.img = fileName;
                    objpost.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
                }
                objWebbanhangEntities.Posts.Add(objpost);
                objWebbanhangEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            var objpost = objWebbanhangEntities.Posts.Where(n => n.Id == id).FirstOrDefault();
            return View(objpost);
        }
        public ActionResult Delete(int id)
        {
            var objPost = objWebbanhangEntities.Posts.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [HttpPost]
        public ActionResult Delete(Post objpost)
        {
            var objPost = objWebbanhangEntities.Posts.Where(n => n.Id == objpost.Id).FirstOrDefault();

            objWebbanhangEntities.Posts.Remove(objpost);
            objWebbanhangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objPost = objWebbanhangEntities.Posts.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [HttpPost]
        public ActionResult Edit(Post objPost)
        {
            if (objPost.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objPost.ImageUpload.FileName);
                String extension = Path.GetExtension(objPost.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objPost.img = fileName;
                objPost.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
            }
            objWebbanhangEntities.Entry(objPost).State = System.Data.Entity.EntityState.Modified;
            objWebbanhangEntities.SaveChanges();
            return View(objPost);

        }
    }
}