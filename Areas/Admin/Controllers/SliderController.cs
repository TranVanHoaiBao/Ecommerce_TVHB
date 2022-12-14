using Ecommerce_TVHB.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Ecommerce_TVHB.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Admin/Slider
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstSlider = new List<Slider>();

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
                lstSlider = objWebbanhangEntities.Sliders.Where(n => n.Title.Contains(SearchString)).ToList();
            }
            else
            {
                lstSlider = objWebbanhangEntities.Sliders.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstSlider.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Slider objslider)
        {
            try
            {
                if (objslider.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objslider.ImageUpload.FileName);
                    String extension = Path.GetExtension(objslider.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objslider.Img = fileName;
                    objslider.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
                }
                objWebbanhangEntities.Sliders.Add(objslider);
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
            var objslider = objWebbanhangEntities.Sliders.Where(n => n.Id == id).FirstOrDefault();
            return View(objslider);
        }
        public ActionResult Delete(int id)
        {
            var objSlider = objWebbanhangEntities.Sliders.Where(n => n.Id == id).FirstOrDefault();
            return View(objSlider);
        }
        [HttpPost]
        public ActionResult Delete(Slider objslider)
        {
            var objSlider = objWebbanhangEntities.Sliders.Where(n => n.Id == objslider.Id).FirstOrDefault();

            objWebbanhangEntities.Sliders.Remove(objslider);
            objWebbanhangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objSlider = objWebbanhangEntities.Sliders.Where(n => n.Id == id).FirstOrDefault();
            return View(objSlider);
        }
        [HttpPost]
        public ActionResult Edit(Slider objSlider)
        {
            if (objSlider.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objSlider.ImageUpload.FileName);
                String extension = Path.GetExtension(objSlider.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objSlider.Img = fileName;
                objSlider.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/images/"), fileName));
            }
            objWebbanhangEntities.Entry(objSlider).State = System.Data.Entity.EntityState.Modified;
            objWebbanhangEntities.SaveChanges();
            return View(objSlider);

        }
    }
}