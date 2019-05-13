using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class AllTestController : Controller
    {
        // GET: AllTest
        public ActionResult Index()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.ListClasses = context.Database.SqlQuery<Class>("SELECT * FROM dbo.Class").ToList();
                //ViewBag.ListClasses = context.Classes.ToList();
            }
                return View();
        }
        
    }
}