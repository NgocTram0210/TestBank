using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listCourses = context.Database.SqlQuery<Cours>("SELECT * FROM dbo.Courses ").ToList();

            }
            return View();
        }
        public ActionResult View(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.Courses = context.Database.SqlQuery<Cours>("SELECT * FROM dbo.Courses where CoursesID="+id).ToList();

            }
            return View();
        }
    }
}