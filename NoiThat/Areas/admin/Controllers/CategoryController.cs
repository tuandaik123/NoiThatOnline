using NoiThat.Controllers;
using NoiThat.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NoiThat.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public CategoryController()
        {
            data = Connect.GetConnect();
        }
        public ActionResult Index(int ? page)
        {
            int iSize = 6;
            int iPage = page ?? 1;
            if (Session["admin"] != null)
            {
                var listDanhMuc = from cd in data.categories select cd;
                return View(listDanhMuc.ToPagedList(iPage,iSize));
            }
            else
            {
                return RedirectToAction("Index", "loginAdmin");
            }
        }

        public ActionResult ViewaddCategory()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            return View();
        }

        public ActionResult ViewUpadateCategory(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var cate = data.categories.SingleOrDefault(c => c.id == id);
            return View(cate);
        }

        public ActionResult addCategory(FormCollection f)
        {
            category ct = new category();
            var name = f["tendm"];
            var mota = f["mota"];
            ct.name = name;
            ct.description = mota;
            data.categories.InsertOnSubmit(ct);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult delete(int id)
        {
            var delete = data.categories.SingleOrDefault(c => c.id == id);
            data.categories.DeleteOnSubmit(delete);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult update(int id, FormCollection f)
        {
            var ct = data.categories.SingleOrDefault(c => c.id == id);

            if (ct != null)
            {
                var name = f["tendm"];
                var mota = f["mota"];

                ct.name = name;
                ct.description = mota;

                data.SubmitChanges();
            }
            return RedirectToAction("Index");
        }

        ////Brand
        public ActionResult IndexBrand(int ? page)
        {
            int size = 6;
            int ipage = page ?? 1;
            if (Session["admin"] != null)
            {
                var listDanhMuc = from cd in data.brands select cd;
                return View(listDanhMuc.ToPagedList(ipage,size));
            }
            else
            {
                return RedirectToAction("Index", "loginAdmin");
            }
        }

        public ActionResult addBrandView()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            return View();
        }

        public ActionResult addBrand(FormCollection f)
        {
            var name = f["name"];
            var mota = f["mota"];
            brand br = new brand();
            br.name = name;
            br.description = mota;
            data.brands.InsertOnSubmit(br);
            data.SubmitChanges();
            return RedirectToAction("IndexBrand");
        }

        public ActionResult deleteBrand(int id)
        {
            var br = data.brands.SingleOrDefault(c => c.id == id);
                data.brands.DeleteOnSubmit(br);
                data.SubmitChanges();
            return RedirectToAction("IndexBrand");
        }

        public ActionResult ViewUpadateBrand(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var cate = data.brands.SingleOrDefault(c => c.id == id);
            return View(cate);
        }

        public ActionResult updateBrand(int id, FormCollection f)
        {
            var ct = data.brands.SingleOrDefault(c => c.id == id);

            if (ct != null)
            {
                var name = f["tendm"];
                var mota = f["mota"];

                ct.name = name;
                ct.description = mota;

                data.SubmitChanges();
            }
            return RedirectToAction("IndexBrand");
        }

        public ActionResult categoryPartial()
        {
            var listDanhMuc = from cd in data.categories select cd;
            return PartialView(listDanhMuc);
        }

        public ActionResult brandPartial()
        {
            var listDanhMuc = from cd in data.brands select cd;
            return PartialView(listDanhMuc);
        }
    }
}