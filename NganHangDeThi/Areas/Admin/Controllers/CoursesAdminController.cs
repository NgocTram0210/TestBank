using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    [ValidateInput(false)]
    public class CoursesAdminController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/Courses
        public ActionResult ListCourse()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listCours = context.Database.SqlQuery<Cours>("Select * from Courses").ToList();
            }
            return View();
        }
        public ActionResult DetailCourse(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var data = context.Database.SqlQuery<Cours>("Select * from Courses where CoursesID =" + id).FirstOrDefault();
                

                return View(data);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var d = db.Courses.Find(id);
                db.Courses.Remove(d);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult formatNumber(object Number)
        {
            string sPrice = Number.ToString();
            if (Number == "")
                return Json("0", JsonRequestBehavior.AllowGet);
            string str = "";
            for (int i = sPrice.Length; i >= 3; i -= 3)
            {
                str = (i == 3 ? "" : ",") + sPrice.Substring(i - 3 < 0 ? 0 : i - 3, 3) + str;
            }
            str = sPrice.Substring(0, sPrice.Length % 3) + str;
            
            return Json(str.Replace("-,", "-") + "", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult doAdd(Cours Cours)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(Cours.ImageFile.FileName);
                string ex = Path.GetExtension(Cours.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
                Cours.Image = filename;
                filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
                Cours.ImageFile.SaveAs(filename);
                Cours.DateOpen = new DateTime(int.Parse(Cours.open.Split('/')[2]), int.Parse(Cours.open.Split('/')[1]), int.Parse(Cours.open.Split('/')[0]));
                Cours.RegistrationDate = new DateTime(int.Parse(Cours.regis.Split('/')[2]), int.Parse(Cours.regis.Split('/')[1]), int.Parse(Cours.regis.Split('/')[0]));
                db.Courses.Add(Cours);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult doEdit(Cours Cours)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var d = db.Courses.Find(Cours.CoursesID);
                    if (Cours.ImageFile != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(Cours.ImageFile.FileName);
                        string ex = Path.GetExtension(Cours.ImageFile.FileName);
                        filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
                        d.Image = filename;
                        filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
                        Cours.ImageFile.SaveAs(filename);
                    }
                    d.Title = Cours.Title;
                    d.DateOpen = Cours.DateOpen;
                    d.Des = Cours.Des;
                    d.RegistrationDate = Cours.RegistrationDate;
                    d.Content = Cours.Content;
                    d.Price = Cours.Price;

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