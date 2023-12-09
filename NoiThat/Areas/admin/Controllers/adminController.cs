using NoiThat.Controllers;
using NoiThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Areas.admin.Controllers
{
    public class adminController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public adminController()
        {
            data = Connect.GetConnect();
        }

        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                ViewBag.tongDH = SumDH();
                ViewBag.tongKH = SumKH();
                ViewBag.tongSP = sumSP();
                ViewBag.dt = CalculateTotalPrice();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "loginAdmin");
            }
        }

        public int SumDH()
        {
            var result = data.orders
                .GroupBy(order => order.id)
                .Select(group => new
                {
                    OrderID = group.Key,
                    TotalOrders = group.Count(),
                })
                .ToList();

            // Giả sử bạn muốn trả về tổng số đơn hàng
            int totalOrders = result.Sum(item => item.TotalOrders);

            return totalOrders;
        }

        public int SumKH()
        {
            var result = data.customers
                .GroupBy(kh => kh.id)
                .Select(group => new
                {
                    OrderID = group.Key,
                    TotalOrders = group.Count(),
                })
                .ToList();

            // Giả sử bạn muốn trả về tổng số đơn hàng
            int total = result.Sum(item => item.TotalOrders);

            return total;
        }
        public int sumSP()
        {
            var result = data.orderDetails.Sum(n => n.quantity);
            return result.Value;
        }
        public int CalculateTotalPrice()
        {
            double? result = data.orders.Sum(n => n.totalPrice);
            int intValue = Convert.ToInt32(result.GetValueOrDefault());
            return intValue;
        }
    }
}