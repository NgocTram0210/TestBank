using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    public class QLDeThiController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/QLDeThi
        public ActionResult Index()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listExam = context.Exams.ToList();
                ViewBag.listClass = context.Classes.ToList();
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var data = context.Database.SqlQuery<Exam>("Select * from Exam where ExamID =" + id).FirstOrDefault();
               
                ViewBag.listClass = context.Classes.ToList();
                var da = context.Database.SqlQuery<string>("Select True from Exam where ExamID =" + id).FirstOrDefault();
                if (da == "A")
                    ViewBag.da = 1;
                else if (da == "B")
                    ViewBag.da = 2;
                else if (da == "C")
                    ViewBag.da = 3;
                else if (da == "D")
                    ViewBag.da = 4;
                else ViewBag.da = null;
                ViewBag.le = context.Database.SqlQuery<int>("Select Level from Exam where ExamID =" + id).FirstOrDefault();
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult CapNhat(Exam exam)
        {
            try
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult Add(Exam exam)
        {
            try
            {
                db.Exams.Add(exam);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        
        public ActionResult Delete(int id)
        {
            try
            {
                var d = db.Exams.Find(6);
                if (d != null) db.Exams.Remove(d);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}