using NoiThat.Controllers;
using NoiThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Areas.admin.Controllers
{
    public class OrderDetailController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public OrderDetailController()
        {
            data = Connect.GetConnect();
        }
        public ActionResult Index(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var lst = data.orderDetails.FirstOrDefault(n => n.order_id == id);
            return View(lst);
        }

        public ActionResult daDuyet(int id)
        {
            var lst = data.orderDetails.FirstOrDefault(n => n.order_id == id);
            return View(lst);
        }

        public ActionResult customersPartial(int id) 
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var lst = data.customers.SingleOrDefault(n => n.id == id);
            return PartialView(lst);
        }

        public ActionResult ProductPartial(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "loginAdmin");
            }
            var listOrderDetails = data.orderDetails.Where(od => od.order_id == id).ToList();
            return PartialView(listOrderDetails);
        }

        public ActionResult XacNhanDon(int id, FormCollection f)
        {
            DateTime ngayGiaoDate = DateTime.Parse(f["ngayGiao"]);
            var od = data.orders.SingleOrDefault(n => n.id == id);
            var listOrderDetails = data.orderDetails.Where(od1 => od1.order_id == id).ToList();
            var kh  = data.customers.SingleOrDefault(n=>n.id == od.customer_id);
            od.date_delivery = ngayGiaoDate;
            od.status = 1;
            od.paymentStatus = 1;
            data.SubmitChanges();
            var fromEmail = "Nội Thất Sky<tiktokofme10022003@gmail.com>";
            var subject = "Đơn hàng";
            string body = "<h2>Đơn Hàng #ĐH" + od.id + " đã giao hàng</2></br>";
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
            body += $"<tr><td colspan='2' style='border: 1px solid #ddd; padding: 8px; text-align: center;'>Ngày giao hàng:</td>" +
                   $"<td style='border: 1px solid #ddd; padding: 8px; text-align: center;'>{ngayGiaoDate.Date.ToString("dd/MM/yyyy")}</td></tr>";
            body += "</table>";
            // Rest of your email sending code
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
            return RedirectToAction("Index", "Order");


        }
    }
}