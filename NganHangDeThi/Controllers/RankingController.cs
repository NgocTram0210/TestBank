using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;
using NganHangDeThi.Models;

namespace NganHangDeThi.Controllers
{
    public class RankingController : Controller
    {
        // GET: Ranking
        public ActionResult Index()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listClass = context.Database.SqlQuery<Class>("SELECT * FROM dbo.Class").ToList();

            }
            return View();
        }
        public JsonResult RankingClass(int cl)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var p = new SqlParameter("@class", cl);
                List<XepHang_Result> dt = context.Database.SqlQuery<XepHang_Result>("XepHang @class", p).ToList();
                ViewBag.Rank = dt;
                return Json(dt, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RankingClassLevel(int cl)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var p = new SqlParameter("@class", cl);
                var lv = new SqlParameter("@lv", 1);
                List<XepHang_Result> dt = context.Database.SqlQuery<XepHang_Result>("XepHangLevel @class, @lv", p,lv).ToList();
                ViewBag.lv1 = dt;

                var p1 = new SqlParameter("@class", cl);
                var lv1 = new SqlParameter("@lv", 2);
                List<XepHang_Result> dt1 = context.Database.SqlQuery<XepHang_Result>("XepHangLevel @class, @lv", p1, lv1).ToList();
                ViewBag.lv2 = dt1;

                var p2 = new SqlParameter("@class", cl);
                var lv2 = new SqlParameter("@lv", 3);
                List<XepHang_Result> dt2 = context.Database.SqlQuery<XepHang_Result>("XepHangLevel @class, @lv", p2, lv2).ToList();
                ViewBag.lv1 = dt2;
                List<XepHang_Result>[] d = new List<XepHang_Result>[3];
                d[0] = dt;
                d[1] = dt1;
                d[2] = dt2;
                return Json(d, JsonRequestBehavior.AllowGet);
            }
        }
    }
}