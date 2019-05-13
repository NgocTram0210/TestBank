using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    public class AboutAdminController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/About
        public ActionResult ListAbout()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listAbout = context.Database.SqlQuery<About>("Select * from About").ToList();
            }
            return View();
        }
        public ActionResult DetalAbout()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var d = db.Abouts.Find(id);
                db.Abouts.Remove(d);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Add(About About)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    DateTime t = DateTime.Now;
                    About.DateAdd = t;
                    db.Abouts.Add(About);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Edit(int id, string title,string des)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var d = db.Abouts.Find(id);
                    d.Title = title;
                    d.Des = des;
                    db.Entry(d).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}