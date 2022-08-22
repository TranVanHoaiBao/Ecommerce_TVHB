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
    public class CategoryController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstCategory = new List<Category>();

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
                lstCategory = objWebbanhangEntities.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objWebbanhangEntities.Categories.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objcategory)
        {
            try
            {
                if (objcategory.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objcategory.ImageUpload.FileName);
                    String extension = Path.GetExtension(objcategory.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objcategory.Img = fileName;
                    objcategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
                }
                objWebbanhangEntities.Categories.Add(objcategory);
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
            var objpost = objWebbanhangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objpost);
        }
        public ActionResult Delete(int id)
        {
            var objPost = objWebbanhangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [HttpPost]
        public ActionResult Delete(Category objcategory)
        {
            var objPost = objWebbanhangEntities.Categories.Where(n => n.Id == objcategory.Id).FirstOrDefault();

            objWebbanhangEntities.Categories.Remove(objcategory);
            objWebbanhangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objPost = objWebbanhangEntities.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objPost);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Category objcategory)
        {
            if (objcategory.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objcategory.ImageUpload.FileName);
                String extension = Path.GetExtension(objcategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objcategory.Img = fileName;
                objcategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
            }
            objWebbanhangEntities.Entry(objcategory).State = System.Data.Entity.EntityState.Modified;
            objWebbanhangEntities.SaveChanges();
            return View(objcategory);

        }
    }
}