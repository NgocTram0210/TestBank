using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    public class QLContactController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/QLContact
        public ActionResult Index(int seen, string title)
        {
            ViewBag.Tl = title;
            using (var context = new NganHangDeThiEntities1())
            {
                if (title == null)
                    title = "";
                bool s = true;
                if(seen == -1)
                    ViewBag.listContact = context.Contacts.Where(x => x.Titles.Contains(title)).ToList();
                else
                {
                    if (seen == 0)
                        s = false;
                    ViewBag.listContact = context.Contacts.Where(x => x.Seen == s).Where(x => x.Titles.EndsWith(title)).ToList();
                }
                ViewBag.Seen = seen;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Up(int id, bool seen)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var data = context.Database.SqlQuery<Contact>("select * from Contact where ContactID=" + id).FirstOrDefault();
                    data.Seen = seen;
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}