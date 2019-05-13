using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class HomeController : Controller
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Index1()
        {
                return View();
        }

        [HttpPost]
        public ActionResult Add(Contact contact)
        {
            try
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Logout()
        {
            try
            {
                Session["ID"] = null;
                Session["Q"] = null;
                Session["Name"] = null;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}