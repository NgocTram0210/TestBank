using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class GuideController : Controller
    {
        // GET: Guide
        public ActionResult Index(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                if (id == 0)
                {
                    int item = context.Database.SqlQuery<int>("Select top 1 GuideGroupID from  dbo.GuideGroup").FirstOrDefault();
                    ViewBag.listGuide = context.Database.SqlQuery<Guide>("SELECT * FROM dbo.Guide where GuideCateID="+item).ToList();
                    ViewBag.ItemGroup = context.Database.SqlQuery<int>("Select top 1 GuideGroupID from  dbo.GuideGroup").LastOrDefault();
                    ViewBag.listGroup = context.Database.SqlQuery<GuideGroup>("SELECT * FROM dbo.GuideGroup").ToList();
                }
                else
                {
                    ViewBag.listGuide = context.Database.SqlQuery<Guide>("SELECT * FROM dbo.Guide where GuideCateID="+id).ToList();
                    ViewBag.ItemGroup = id;
                    ViewBag.listGroup = context.Database.SqlQuery<GuideGroup>("SELECT * FROM dbo.GuideGroup").ToList();
                }
            }
            return View();
        }

        public ActionResult Detail(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.Guide = context.Database.SqlQuery<Guide>("SELECT * FROM dbo.Guide where ID=" + id).ToList();
                ViewBag.listGroup = context.Database.SqlQuery<GuideGroup>("SELECT * FROM dbo.GuideGroup").ToList();
                ViewBag.ItemGroup = context.Database.SqlQuery<int>("Select GuideCateID from  dbo.Guide where ID="+id).FirstOrDefault();
            }
            return View();
        }
    }
}