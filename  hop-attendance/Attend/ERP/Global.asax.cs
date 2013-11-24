using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate.Context;
using NHibernate;

namespace ERP
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
        public static Dictionary<string, DateTime> ConnectedtUsers { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new ERP.Controllers.BaseController.MyExpirePageActionFilterAttribute()); ///*** After Logout Browse Back Key Disable

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ConnectedtUsers = new Dictionary<string, DateTime>(); 
          //  Bootstrapper.Setup();
        }

        public override void Init()
        {
            base.Init();
            //WireUpSessionLifecycle();
        }

        

       

        
    }
}