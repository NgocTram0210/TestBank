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
    public class NewsAdminController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/News
        public ActionResult CateNews()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listCate = context.Database.SqlQuery<NewsCate>("Select * from NewsCate").ToList();
            }
            return View();
        }
        public ActionResult ListNews()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listNews = context.Database.SqlQuery<News>("Select * from News").ToList();
            }
            return View();
        }
        public ActionResult EditNews(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var data = context.Database.SqlQuery<News>("Select * from News where NewsID =" + id).FirstOrDefault();

                ViewBag.listCate = context.NewsCates.ToList();

                return View(data);
            }
        }
        //Select sum post of a cate
        public ActionResult SumNews(int i)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    int sum = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM dbo.News WHERE Cate=" + i).FirstOrDefault();
                    return Json(sum, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        //Select title cate of a post
        public ActionResult ViewCate(int i)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    string name = context.Database.SqlQuery<string>("SELECT b.Title FROM dbo.News a, dbo.NewsCate b WHERE a.Cate = b.NewsCateID AND a.NewsID=" + i).FirstOrDefault();
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCate(int id)
        {
            try
            {
                var d = db.NewsCates.Find(id);
                using (var context = new NganHangDeThiEntities1())
                {
                    int sum = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM dbo.News WHERE Cate=" + id).FirstOrDefault();
                    if (sum > 0)
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        db.NewsCates.Remove(d);
                        db.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AddCate(NewsCate news)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    DateTime t = DateTime.Now;
                    news.UserAdd = (Session["ID"] == null ? 0 : Convert.ToInt32(Session["ID"]));
                    news.DateAdd = t;
                    db.NewsCates.Add(news);
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
        public ActionResult EditCate(int id, string title)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var d = db.NewsCates.Find(id);
                    d.Title = title;
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

        public ActionResult DeletePost(int id)
        {
            try
            {
                var d = db.News.Find(id);
                db.News.Remove(d);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        
        public ActionResult AddPost(News news)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(news.ImageFile.FileName);
                string ex = Path.GetExtension(news.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
                news.Image = filename;
                filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
                news.ImageFile.SaveAs(filename);
                DateTime t = DateTime.Now;
                news.DateAdd = t;
                db.News.Add(news);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult doEdit (News news)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var d = db.News.Find(news.NewsID);
                    if (news.ImageFile != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(news.ImageFile.FileName);
                        string ex = Path.GetExtension(news.ImageFile.FileName);
                        filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
                        d.Image = filename;
                        filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
                        news.ImageFile.SaveAs(filename);
                    }
                    d.Title = news.Title;
                    d.Cate = news.Cate;
                    d.Des = news.Des;

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