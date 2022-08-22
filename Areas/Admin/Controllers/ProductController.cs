using Ecommerce_TVHB.ContactDB;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Ecommerce_TVHB.Common;

namespace Ecommerce_TVHB.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebbanhangEntities objWebbanhangEntities = new WebbanhangEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstProduct = new List<Product>();

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
                lstProduct = objWebbanhangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWebbanhangEntities.Products.ToList();
            }

            ViewBag.CurrentFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            Common objCommon = new Common();
            //Lấy dữ liệu danh mục dưới DB
            var lstCat = objWebbanhangEntities.Categories.ToList();
            //Convert sang list dạng value, text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            //lấy dl thương hiệu dưới DB
            var lstBrand = objWebbanhangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dangj value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá ";
            lstProductType.Add(objProductType);


            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất ";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //convert sang select list dangj value, text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product objproduct)
        {
            try
            {
                if (objproduct.ImageUpload != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);
                    String extension = Path.GetExtension(objproduct.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objproduct.Img = fileName;
                    objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
                }
                if (objproduct.ImageUpload1 != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload1.FileName);
                    String extension = Path.GetExtension(objproduct.ImageUpload1.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objproduct.Img = fileName;
                    objproduct.ImageUpload1.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
                }
                if (objproduct.ImageUpload2 != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload2.FileName);
                    String extension = Path.GetExtension(objproduct.ImageUpload2.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objproduct.Img = fileName;
                    objproduct.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
                }
                if (objproduct.ImageUpload3 != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload3.FileName);
                    String extension = Path.GetExtension(objproduct.ImageUpload3.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objproduct.Img = fileName;
                    objproduct.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
                }
                objWebbanhangEntities.Products.Add(objproduct);
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
            var objProduct = objWebbanhangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        public ActionResult Delete(int id)
        {
            var objProduct = objWebbanhangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objproduct)
        {
            var objProduct = objWebbanhangEntities.Products.Where(n => n.Id == objproduct.Id).FirstOrDefault();

            objWebbanhangEntities.Products.Remove(objProduct);
            objWebbanhangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWebbanhangEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product objproduct)
        {
            if (objproduct.ImageUpload != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload.FileName);
                String extension = Path.GetExtension(objproduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objproduct.Img = fileName;
                objproduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }
            if (objproduct.ImageUpload1 != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload1.FileName);
                String extension = Path.GetExtension(objproduct.ImageUpload1.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objproduct.Img = fileName;
                objproduct.ImageUpload1.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }
            if (objproduct.ImageUpload2 != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload2.FileName);
                String extension = Path.GetExtension(objproduct.ImageUpload2.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objproduct.Img = fileName;
                objproduct.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }
            if (objproduct.ImageUpload3 != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(objproduct.ImageUpload3.FileName);
                String extension = Path.GetExtension(objproduct.ImageUpload3.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objproduct.Img = fileName;
                objproduct.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/Public/"), fileName));
            }
            objWebbanhangEntities.Entry(objproduct).State = System.Data.Entity.EntityState.Modified;
            objWebbanhangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}