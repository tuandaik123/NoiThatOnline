using NoiThat.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Controllers
{
    public class NoiThatOnlineController : Controller
    {
        // GET: NoiThatOnline
        public dbNoiThatOnlineDataContext data;

        public NoiThatOnlineController()
        {
            data = Connect.GetConnect();
        }
        public ActionResult newProduct()
        {
            var listSpMoi = LaySpMoi(6);
            return PartialView(listSpMoi);
        }
        public ActionResult Index()
        {
            var listSpMoi = data.products.ToList();
            return View(listSpMoi);
        }
        public ActionResult danhMucPartial()
        {
            var listDanhMuc = from cd in data.categories select cd;
            return PartialView(listDanhMuc);
        }
        public ActionResult sloganPartial()
        {
            var list = from cd in data.Slogans select cd;
            return PartialView(list);
        }
        private List<product> LaySpMoi(int count)
        {
            return data.products.OrderByDescending(a => a.ngayCapNhap).Take(count).ToList();
        }
        public ActionResult SanPhamTheoDanhMuc(int id, int ? page)
        {
            ViewBag.MaSP = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sp = from c in data.products where c.category_id == id select c;
            return View(sp.ToPagedList(iPageNum,iSize));
        }
        public ActionResult chiTietSanPham(int id)
        {
            var sp = data.products.FirstOrDefault(p => p.id == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult timKienSanPham(FormCollection f)
        {
            var find = f["find"];
            var results = data.products.Where(p => p.name.Contains(find)).ToList();
            return View(results);
        }
    }
}