using System.Web.Mvc;

namespace NoiThat.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                defaults: new { controller = "admin", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}