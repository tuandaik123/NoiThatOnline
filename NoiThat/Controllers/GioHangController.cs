using NoiThat.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Schema;
using VNPAY_CS_ASPX;

namespace NoiThat.Controllers
{
    public class GioHangController : Controller
    {
        public dbNoiThatOnlineDataContext data;

        public GioHangController()
        {
            data = Connect.GetConnect();
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst == null)
            {
                lst = new List<GioHang>();
                Session["GioHang"] = lst;
            }
            return lst;
        }

        public ActionResult ThemGioHang(int ms, string url, FormCollection f)
        {
                List<GioHang> lst = LayGioHang();
                GioHang gh = lst.Find(n => n.iProductId == ms);

                if (gh == null)
                {
                    gh = new GioHang(ms);
                    lst.Add(gh);
                    gh.quantity = int.Parse(f["txtSoLuong"].ToString());
                }
                else
                {
                    gh.quantity = gh.quantity+ int.Parse(f["txtSoLuong"].ToString());
                    
                }
                return Redirect(url);
        }

        public ActionResult ThemGioHangtt(int ms, string url)
        {

                List<GioHang> lst = LayGioHang();
                GioHang gh = lst.Find(n => n.iProductId == ms);

                if (gh == null)
                {
                    gh = new GioHang(ms);
                    lst.Add(gh);
                }
                else
                {
                    gh.quantity++;

                }
                return Redirect(url);
        }
        public int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tsl = lst.Sum(n => n.quantity);
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tt = lst.Sum(n => n.totalPrice);
            }
            return tt;
        }

        public ActionResult GioHang(int? page)
        {
            int size = 3;
            int ipage = page ?? 1;
            List<GioHang> lst = LayGioHang();
            if (lst.Count == 0)
            {
                return RedirectToAction("Index", "NoiThatOnline");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lst.ToPagedList(ipage, size));
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong1 = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult XoaSpKhoiGioHang(int iPr)
        {
            List<GioHang> lst = LayGioHang();
            GioHang sp = lst.SingleOrDefault(n => n.iProductId == iPr);
            if (sp != null)
            {
                lst.RemoveAll(n => n.iProductId == iPr);
                if (lst.Count == 0)
                {
                    return RedirectToAction("Index", "NoiThatOnline");
                }
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int ms, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iProductId == ms);
            if (sp != null)
            {
                sp.quantity = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult DatHang(FormCollection f)
        {
            if (Session["TaiKhoan"] != null && !string.IsNullOrEmpty(Session["TaiKhoan"].ToString()))
            {
                string paymentMethod = f["paymentMethod"];
                string paymentType = f["paymentType"];
                customer kh = (customer)Session["TaiKhoan"];
                order od = new order();
                List<GioHang> lst = LayGioHang();
                if (paymentMethod.Equals("ocd"))
                {
                    od.customer_id = kh.id;
                    od.date_create = DateTime.Now;
                    od.status = 0;
                    od.paymentStatus = 0;
                    od.paymentMethod = "Thanh toán khi nhận hàng";
                    data.orders.InsertOnSubmit(od);
                    data.SubmitChanges();

                    foreach (var gioHangItem in lst)
                    {
                        orderDetail odd = new orderDetail();
                        odd.order_id = od.id;
                        odd.product_id = gioHangItem.iProductId;
                        odd.quantity = gioHangItem.quantity;
                        odd.price = gioHangItem.price;

                        data.orderDetails.InsertOnSubmit(odd);
                    }

                    od.totalPrice = TongTien();
                    data.SubmitChanges();
                    Session["GioHang"] = null;
                    return RedirectToAction("Index", "NoiThatOnline");
                }
                else
                {
                    od.customer_id = kh.id;
                    od.date_create = DateTime.Now;
                    od.status = 0;
                    od.paymentStatus = 0;
                    od.paymentMethod = "Chuyển khoản";
                    data.orders.InsertOnSubmit(od);
                    data.SubmitChanges();

                    foreach (var gioHangItem in lst)
                    {
                        orderDetail odd = new orderDetail();
                        odd.order_id = od.id;
                        odd.product_id = gioHangItem.iProductId;
                        odd.quantity = gioHangItem.quantity;
                        odd.price = gioHangItem.price;

                        data.orderDetails.InsertOnSubmit(odd);
                    }

                    od.totalPrice = TongTien();
                    data.SubmitChanges();
                    var url = UrlPayment(paymentType, od.id);
                    return Redirect(url);
                }
            }
            else
            {
                return RedirectToAction("TaiKhoan", "User");
            }
        }

        public ActionResult PaymentReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; 
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                int orderId = Convert.ToInt32(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];
                var od = data.orders.FirstOrDefault(n => n.id == orderId);
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        od.paymentStatus = 1;
                        od.date_delivery = DateTime.Now;
                        data.SubmitChanges();
                        ViewBag.Msg = "Thanh toán của bạn đã thành công";
                        ViewBag.icon = "http://pluspng.com/img-png/success-png-png-svg-512.png";
                        ViewBag.color = "rgb(65, 209, 161)";
                        Session["GioHang"] = null;
                    }
                    else
                    {
                        data.orders.DeleteOnSubmit(od);
                        data.SubmitChanges();
                        ViewBag.Msg = "Có lỗi xảy ra trong quá trình xử lý";
                        ViewBag.icon = "https://images.onlinelabels.com/images/clip-art/molumen/molumen_red_round_error_warning_icon.png";
                        ViewBag.color = "red";
                    }
                    ViewBag.IDweb = "Mã Website (Terminal ID) : " + TerminalID;
                    ViewBag.SymbolsPayment = "Mã giao dịch thanh toán : " + orderId.ToString();
                    ViewBag.SymbolsVNPAY = "Mã giao dịch tại VNPAY : " + vnpayTranId.ToString();
                    ViewBag.totalPrice = "Số tiền thanh toán : " + vnp_Amount.ToString("0,000,000 ")+"VND";
                    ViewBag.bank = "Ngân hàng thanh toán : " + bankCode;
                }
                else
                {
                    data.orders.DeleteOnSubmit(od);
                    data.SubmitChanges();
                    ViewBag.Msg = "Có lỗi xảy ra trong quá trình xử lý";
                    ViewBag.icon = "https://images.onlinelabels.com/images/clip-art/molumen/molumen_red_round_error_warning_icon.png";
                    ViewBag.color = "red";
                }
            }
            return View();
        }

        public string UrlPayment(string paymentType, int idOrder)
        {
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"];
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"];
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];

            //Get payment input
            var order = data.orders.FirstOrDefault(o => o.id == idOrder);
            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)order.totalPrice*100).ToString());
            if (paymentType.Equals("VNPAYQR"))
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (paymentType.Equals("VNBANK"))
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (paymentType.Equals("INTCARD"))
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            DateTime createDate = Convert.ToDateTime(order.date_create);
            vnpay.AddRequestData("vnp_CreateDate", createDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.id);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.id.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }

    }
}
