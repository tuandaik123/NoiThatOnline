using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat.Models
{
    public class Connect
    {
        public dbNoiThatOnlineDataContext data;
        public static dbNoiThatOnlineDataContext GetConnect()
        {
            string connectionString = "Data Source=LAPTOP-ODTA635P\\SQLEXPRESS01;Initial Catalog=dbNoiThat;Integrated Security=True";
            //string connectionString = "Data Source=SQL8006.site4now.net;Initial Catalog=db_aa16f8_login;User ID=db_aa16f8_login_admin;Password=@BCxyz123";
            dbNoiThatOnlineDataContext dataContext = new dbNoiThatOnlineDataContext(connectionString);
            return dataContext;
        }
    }
}