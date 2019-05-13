using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Controllers
{
    public class TestLoginController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (Session["ID"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
              new System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }

            base.OnActionExecuted(filterContext);
        }
    }
}