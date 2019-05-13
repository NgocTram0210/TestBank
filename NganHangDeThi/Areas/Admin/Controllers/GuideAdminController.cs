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
    public class GuideAdminController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/Guide
        public ActionResult CateGuide()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listCate = context.Database.SqlQuery<GuideGroup>("Select * from GuideGroup").ToList();
            }
            return View();
        }
        public ActionResult ListGuide()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listGuide = context.Database.SqlQuery<Guide>("Select * from Guide").ToList();
            }
            return View();
        }
        public ActionResult EditGuide(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var data = context.Database.SqlQuery<Guide>("Select * from Guide where ID =" + id).FirstOrDefault();

                ViewBag.listCate = context.GuideGroups.ToList();

                return View(data);
            }
        }
        //Select sum post of a cate
        public ActionResult SumPost(int i)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    int sum = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM dbo.Guide WHERE GuideCateID=" + i).FirstOrDefault();
                    return Json(sum, JsonRequestBehavior.AllowGet);
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
                var d = db.GuideGroups.Find(id);
                using (var context = new NganHangDeThiEntities1())
                {
                    int sum = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM dbo.Guide WHERE GuideCateID=" + id).FirstOrDefault();
                    if (sum > 0)
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        db.GuideGroups.Remove(d);
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
        public ActionResult AddCate(GuideGroup cate)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    DateTime t = DateTime.Now;
                    cate.UserAdd = (Session["ID"] == null ? 0 : Convert.ToInt32(Session["ID"]));
                    cate.DateAdd = t;
                    db.GuideGroups.Add(cate);
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
                    var d = db.GuideGroups.Find(id);
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
        //Select title cate of a post
        public ActionResult ViewCate(int i)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    string name = context.Database.SqlQuery<string>("SELECT b.Title FROM dbo.Guide a, dbo.GuideGroup b WHERE a.GuideCateID = b.GuideGroupID AND a.ID=" + i).FirstOrDefault();
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeletePost(int id)
        {
            try
            {
                var d = db.Guides.Find(id);
                db.Guides.Remove(d);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult AddPost(Guide guide)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(guide.ImageFile.FileName);
                string ex = Path.GetExtension(guide.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
                guide.Image = filename;
                filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
                guide.ImageFile.SaveAs(filename);
                DateTime t = DateTime.Now;
                guide.Date = t;
                db.Guides.Add(guide);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult doEdit(Guide guide)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var d = db.Guides.Find(guide.ID);
                    if (guide.ImageFile != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(guide.ImageFile.FileName);
                        string ex = Path.GetExtension(guide.ImageFile.FileName);
                        filename = filename + DateTime.Now.ToString("ddMMyyyy") + ex;
                        d.Image = filename;
                        filename = Path.Combine(Server.MapPath("~/Images/News/"), filename);
                        guide.ImageFile.SaveAs(filename);
                    }
                    d.Title = guide.Title;
                    d.GuideCateID = guide.GuideCateID;
                    d.Des = guide.Des;

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