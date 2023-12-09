using NoiThat.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat.Models
{
    public class GioHang
    {
        public int iProductId { get; set; }
        public string sName { get; set; }
        public string sImage { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get { return quantity * price; } }
        public dbNoiThatOnlineDataContext data;

     

        public GioHang(int ms)
        {
            data = Connect.GetConnect();
            iProductId = ms;
            product s = data.products.Single(n => n.id == iProductId);
            sName = s.name;
            sImage = s.image;
            price = double.Parse(s.price.ToString());
            quantity = 1;
        }

    }
}