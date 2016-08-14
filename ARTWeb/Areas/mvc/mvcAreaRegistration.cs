using System.Web.Mvc;

namespace SkyStem.ART.Web.Areas.mvc
{
    public class mvcAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "mvc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.Routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
            //context.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            context.MapRoute(
                "login",
                "login",
                new { controller = "login", action = "Index" }
            );

            context.MapRoute(
                "mvc_default",
                "app/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}