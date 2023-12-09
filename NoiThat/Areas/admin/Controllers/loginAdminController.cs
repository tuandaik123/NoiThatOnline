using NoiThat.Controllers;
using NoiThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Areas.admin.Controllers
{
    public class loginAdminController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public loginAdminController()
        {
            data = Connect.GetConnect();
        }

        // GET: admin/loginAdmin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f) 
        {
            var username = f["userName"];
            var password = f["password"];
            var ad = data.admins.SingleOrDefault(n => n.username == username && n.password == password);
            if (ad != null)
            {
                Session["admin"] = ad;
                return RedirectToAction("Index", "admin");
            }
            else
            {
                ViewBag.failLogin = "Đăng nhập thất bại";
                return RedirectToAction("Index");
            }
        }
    }
}