using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                if (id == 0)
                {
                    int item = context.Database.SqlQuery<int>("Select top 1 AboutID from  dbo.About").FirstOrDefault();
                    ViewBag.About = context.Database.SqlQuery<About>("SELECT * FROM dbo.About where AboutID=" + item).ToList();
                    ViewBag.listAbout = context.Database.SqlQuery<About>("SELECT * FROM dbo.About ").ToList();
                    ViewBag.ItemGroup = context.Database.SqlQuery<int>("Select top 1 AboutID from  dbo.About").LastOrDefault();
                }
                else
                {
                    ViewBag.ItemGroup = context.Database.SqlQuery<int>("Select top 1 AboutID from  dbo.About").LastOrDefault();
                    ViewBag.About = context.Database.SqlQuery<About>("SELECT * FROM dbo.About where AboutID=" + id).ToList();
                    ViewBag.listAbout = context.Database.SqlQuery<About>("SELECT * FROM dbo.About ").ToList();
                }
            }
            return View();
        }
    }
}