using NoiThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using NoiThat.Controllers;

namespace NoiThat.Areas.admin.Controllers
{
    public class OrderController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public OrderController()
        {
            data = Connect.GetConnect();
        }
        public ActionResult Index(int ? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            int iSize = 6;
            int iPage = page ?? 1;
            var lst = data.orders.Where(od => od.status == 0).ToList();
            return View(lst.ToPagedList(iPage, iSize));
        }

        public ActionResult daDuyet(int ? page)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            int iSize = 6;
            int iPage = page ?? 1;
            var lst = data.orders.Where(od => od.status == 1).ToList();
            return View(lst.ToPagedList(iPage,iSize));
        }

        public ActionResult deleteView(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var od = data.orders.SingleOrDefault(n=>n.id== id);
            return View(od);
        }

        public ActionResult delete(int id,FormCollection f) 
        {
            var od = data.orders.SingleOrDefault(n => n.id == id);
            var kh = data.customers.SingleOrDefault(n => n.id == od.customer_id);
            var listOrderDetails = data.orderDetails.Where(od1 => od1.order_id == id).ToList();
            var liDo = f["lyDo"];
            data.orders.DeleteOnSubmit(od);
            data.SubmitChanges();
            var fromEmail = "Nội Thất Sky<tiktokofme10022003@gmail.com>";
            var subject = "Hủy đơn hàng";

            var body = "Đơn hàng #DH" + od.id + " đã bị hủy\n";
            body += "\nLý do hủy đơn : " + liDo + "\n";
            body += "<table style='border-collapse: collapse; width: 100%; border: 1px solid #ddd;'>" +
              "<tr><th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Sản phẩm</th>" +
              "<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Số lượng</th>" +
              "<th style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Giá</th></tr>";
            foreach (var orderDetail in listOrderDetails)
            {
                var product = data.products.SingleOrDefault(p => p.id == orderDetail.product_id);
                if (product != null)
                {
                    body += $"<tr><td style='border: 1px solid #ddd; padding: 8px; text-align: center; font-weight: normal;'>{product.name}</td>" +
                            $"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; font-weight: normal;'>{orderDetail.quantity}</td>" +
                            $"<td style='border: 1px solid #ddd; padding: 8px; text-align: center; font-weight: normal;'>{orderDetail.price:#,##} VND</td></tr>";
                }
            }
            body += $"<tr><td colspan='2' style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Tổng hóa đơn:</td>" +
                   $"<td style='border: 1px solid #ddd; padding: 8px; text-align: center;'>{od.totalPrice:#,##} VND</td></tr>";
            body += "</table>";

            using (var mail = new MailMessage(fromEmail, kh.email))
            {
                mail.Subject = subject;
                mail.Body = body;

                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("tiktokofme10022003@gmail.com", "msmdawujowxaddqd");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return RedirectToAction("Index");
        }
    }
}