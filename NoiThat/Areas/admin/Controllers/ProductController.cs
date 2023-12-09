using NoiThat.Controllers;
using NoiThat.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Areas.admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: admin/Product
        public dbNoiThatOnlineDataContext data;

        public ProductController()
        {
             data = Connect.GetConnect();
        }
        public ActionResult Index(int ? page)
        {
            int iSize = 6;
            int iPageNum = (page ?? 1);
            var lst = from cd in data.products select cd;
            if (Session["admin"] != null)
            {
                return View(lst.ToPagedList(iPageNum,iSize));
            }
            else
            {
                return RedirectToAction("Index", "loginAdmin");
            }
        }
        public ActionResult delete(int id) 
        {
            var pr = data.products.SingleOrDefault(x => x.id == id);
            string imagePath = Path.Combine(Server.MapPath("~/Images"), pr.image);
            System.IO.File.Delete(imagePath);
            data.products.DeleteOnSubmit(pr);
            data.SubmitChanges();
            return RedirectToAction("Index");

        }

        public ActionResult viewAddProduct()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(FormCollection f, HttpPostedFileBase image)
        {
            string namefile = GenerateRandomString(65);
            string fileName = "product="+namefile + ".jpg";
            string path = Path.Combine(Server.MapPath("~/Images"), fileName);
            image.SaveAs(path);
            var name = f["name"];
            var mota = f["mota"];
            var price = f["price"];
            var category = int.Parse(f["category"]);
            var brand = int.Parse(f["brand"]);
            product pr = new product();
            pr.name = name;
            pr.description = mota;
            pr.image = fileName;
            pr.price = double.Parse(price);
            pr.brand_id = brand;
            pr.category_id = category;
            pr.ngayCapNhap = DateTime.Now;
            data.products.InsertOnSubmit(pr);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }


        public ActionResult viewUpdateProduct(int id)
        {
            var pr = data.products.SingleOrDefault(n=> n.id == id);
            return View(pr);
        }

        [HttpPost]
        public ActionResult updateProduct(int id, FormCollection f, HttpPostedFileBase image)
        {
            var name = f["name"];
            var mota = f["mota"];
            var price = f["price"];
            var category = int.Parse(f["category"]);
            var brand = int.Parse(f["brand"]);
            var img = f["img"];
            var pr = data.products.SingleOrDefault(n => n.id == id);
            if (image != null && image.ContentLength > 0)
            {
                string namefile = GenerateRandomString(65);
                string fileName = "product=" + namefile + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                image.SaveAs(path);
                string imagePath = Path.Combine(Server.MapPath("~/Images"), pr.image);
                System.IO.File.Delete(imagePath);
                pr.name = name;
                pr.description = mota;
                pr.image = fileName;
                pr.price = double.Parse(price);
                pr.brand_id = brand;
                pr.category_id = category;
                pr.ngayCapNhap = DateTime.Now;
                data.SubmitChanges();
            }
            else
            {
                pr.name = name;
                pr.description = mota;
                pr.image = img;
                pr.price = double.Parse(price);
                pr.brand_id = brand;
                pr.category_id = category;
                pr.ngayCapNhap = DateTime.Now;
                data.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
        // Function to generate a random string
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}