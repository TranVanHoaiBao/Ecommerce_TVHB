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
    public class BrandController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstBrand = new List<Brand>();

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
                lstBrand = objWebbanhangEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objWebbanhangEntities.Brands.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objbrand)
        {
            try
            {
                if (objbrand.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objbrand.ImageUpload.FileName);
                    String extension = Path.GetExtension(objbrand.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objbrand.Img = fileName;
                    objbrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
                }
                objWebbanhangEntities.Brands.Add(objbrand);
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
            var objbrand = objWebbanhangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objbrand);
        }
        public ActionResult Delete(int id)
        {
            var objBrand = objWebbanhangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objbrand)
        {
            var objBrand = objWebbanhangEntities.Brands.Where(n => n.Id == objbrand.Id).FirstOrDefault();

            objWebbanhangEntities.Brands.Remove(objbrand);
            objWebbanhangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objBrand = objWebbanhangEntities.Brands.Where(n => n.Id == id).FirstOrDefault();
            return View(objBrand);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Brand objbrand)
        {
            if (objbrand.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objbrand.ImageUpload.FileName);
                String extension = Path.GetExtension(objbrand.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objbrand.Img = fileName;
                objbrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
            }
            objWebbanhangEntities.Entry(objbrand).State = System.Data.Entity.EntityState.Modified;
            objWebbanhangEntities.SaveChanges();
            return View(objbrand);

        }
    }
}