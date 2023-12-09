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
    public class SloganController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public SloganController()
        {
            data = Connect.GetConnect();
        }
        public ActionResult Index(int ? page)
        {
            int size = 6;
            int Page = page ?? 1;
            var lst = from cd in data.Slogans select cd;
            if (Session["admin"] != null)
            {
                return View(lst.ToPagedList(Page,size));
            }
            else
            {
                return RedirectToAction("Index", "loginAdmin");
            }
        }

        public ActionResult addSloganView()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            return View();
        }

        public ActionResult updateSloganView(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var sl = data.Slogans.SingleOrDefault(s => s.id == id);
            return View(sl);
        }

        public ActionResult addSlogan(FormCollection f)
        {
            var nd = f["slogan"];
            Slogan slogan = new Slogan();
            slogan.noi_dung = nd;
            data.Slogans.InsertOnSubmit(slogan);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult delete(int id)
        {
            var sl = data.Slogans.SingleOrDefault(n => n.id == id);
                data.Slogans.DeleteOnSubmit(sl);
                data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult updateSlogan(int id,FormCollection f)
        {
            var sl = data.Slogans.SingleOrDefault(s => s.id == id);
            var nd = f["slogan"];
            if (nd != null)
            {
                sl.noi_dung = nd;
                data.SubmitChanges();
            }
            return RedirectToAction("Index");
        }

    }
}