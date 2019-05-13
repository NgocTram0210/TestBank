using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (Session["ID"] == null || Convert.ToInt32(Session["Q"]) != 1)
            {
                filterContext.Result = new RedirectResult("~/Login");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}