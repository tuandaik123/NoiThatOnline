using Microsoft.Ajax.Utilities;
using NoiThat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NoiThat.Controllers
{
    public class UserController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public UserController()
        {
            data = Connect.GetConnect();
        }
        [HttpGet]
        public ActionResult TaiKhoan()
        {
            if (Session["TaiKhoan"] != null)
            {
               return RedirectToAction("Index", "NoiThatOnline");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TaiKhoan(FormCollection collection)
        {
            var dn = collection["action"];
            if (dn == "register")
            {
                var stenDN = collection["TenDN"];
                var sMatkhau = collection["Matkhau"];
                var sMatkhauNhapLai = collection["MatkhauNL"];
                var sDiachi = collection["DiaChi"];
                var sEmail = collection["Email"];
                var sDienThoai = collection["DienThoai"];

                if (data.customers.SingleOrDefault(n => n.email == stenDN) != null)
                {
                    ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
                    ViewBag.cl = "red";
                }
                else if (data.customers.SingleOrDefault(n => n.email == sEmail) != null)
                {
                    ViewBag.ThongBao = "Email đã được sử dụng";
                    ViewBag.cl = "red";
                }
                else
                {
                    customer kh = new customer
                    {
                        name = stenDN,
                        email = sEmail,
                        phone = sDienThoai,
                        address = sDiachi,
                        password = sMatkhau,
                    };

                    data.customers.InsertOnSubmit(kh);
                    data.SubmitChanges();
                    ViewBag.ThongBao = "Đăng kí tài khoản thành công";
                    ViewBag.cl = "green";
                    return View();
                }
            }
            else if (dn == "login")
            {
                var username = collection["userName"];
                var password = collection["passWord"];
                if (String.IsNullOrEmpty(username))
                {
                    ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
                }
                else if (String.IsNullOrEmpty(password))
                {
                    ViewData["Err2"] = "Phải nhập mật khẩu";
                }
                else
                {
                    customer kh = data.customers.SingleOrDefault(n => n.email == username && n.password == password);

                    if (kh != null)
                    {
                        Session["TaiKhoan"] = kh;
                        return RedirectToAction("Index", "NoiThatOnline");
                    }
                    else
                    {
                        ViewBag.ThongBao = "Email hoặc mật khẩu không đúng";
                        return View();

                    }

                }
            }
            return this.TaiKhoan();
        }

        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            Session.Remove("GioHang");
            return RedirectToAction("Index", "NoiThatOnline");
        }

        public ActionResult quenMK()
        {
            if (Session["TaiKhoan"] != null)
            {
                return RedirectToAction("Index", "NoiThatOnline");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email11)
        {
            customer kh = data.customers.SingleOrDefault(n => n.email == email11);

            if (kh == null)
            {
                ViewBag.Message = "Email không tồn tại.";
                return View("quenMK");
            }

            string newPass = GenerateNewPassword();
            kh.password = newPass;
            data.SubmitChanges();

            SendNewPasswordEmail(email11, newPass);

            ViewBag.Message = "Mật khẩu mới đã được gửi đến email của bạn.";
            return RedirectToAction("TaiKhoan", "User");
        }

        private string GenerateNewPassword()
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int passwordLength = 8;

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomData = new byte[passwordLength];
                rng.GetBytes(randomData);

                char[] password = new char[passwordLength];

                for (int i = 0; i < passwordLength; i++)
                {
                    password[i] = validChars[randomData[i] % validChars.Length];
                }

                return new string(password);
            }
        }


        // Hàm gửi email với mật khẩu mới
        private void SendNewPasswordEmail(string email11, string newPassword)
        {
            var fromEmail = "Nội Thất Sky<tiktokofme10022003@gmail.com>";
            var subject = "Mật khẩu mới";

            var body = "Mật khẩu mới của bạn là: " + newPassword;

            using (var mail = new MailMessage(fromEmail, email11))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("tiktokofme10022003@gmail.com", "msmdawujowxaddqd");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}