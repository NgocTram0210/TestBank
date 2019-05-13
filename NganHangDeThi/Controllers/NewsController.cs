using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                if (id == 0)
                {
                    int item = context.Database.SqlQuery<int>("Select top 1 NewsCateID from  dbo.NewsCate").FirstOrDefault();
                    ViewBag.listNews = context.Database.SqlQuery<News>("SELECT * FROM dbo.News where Cate=" + item).ToList();
                    ViewBag.ItemGroup = context.Database.SqlQuery<int>("Select top 1 NewsCateID from  dbo.NewsCate").LastOrDefault();
                    ViewBag.listGroup = context.Database.SqlQuery<NewsCate>("SELECT * FROM dbo.NewsCate").ToList();
                }
                else
                {
                    ViewBag.listNews = context.Database.SqlQuery<News>("SELECT * FROM dbo.News where Cate=" + id).ToList();
                    ViewBag.ItemGroup = id;
                    ViewBag.listGroup = context.Database.SqlQuery<NewsCate>("SELECT * FROM dbo.NewsCate").ToList();
                }
            }
            return View();
        }
        public ActionResult DetailNews(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listNews = context.Database.SqlQuery<News>("SELECT * FROM dbo.News where NewsID=" + id).ToList();
                ViewBag.listGroup = context.Database.SqlQuery<NewsCate>("SELECT * FROM dbo.NewsCate").ToList();
                ViewBag.ItemGroup = context.Database.SqlQuery<int>("Select Cate from  dbo.News where NewsID=" + id).FirstOrDefault();
            }
            return View();
        }
    }
}