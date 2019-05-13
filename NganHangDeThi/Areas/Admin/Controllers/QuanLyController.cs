using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    public class QuanLyController : BaseController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Admin/QuanLy
        public ActionResult Index()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listAccount = context.Accounts.Where(x => x.Decentralization == 1).Where(x => x.Status == true).ToList();
            }
                return View();
        }

        public ActionResult KhachHang()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                ViewBag.listAccount = context.Accounts.Where(x => x.Decentralization == 2).ToList();
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var data = context.Database.SqlQuery<Account>("Select * from Account where AccountID =" + id).FirstOrDefault();

                ViewBag.listDecentralization = context.Decentralizations.ToList();
                
                return View(data);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var d = db.Accounts.Find(id);
                db.Accounts.Remove(d);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Add(Account account)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var data = context.Database.SqlQuery<int>("Select count(*) from Account where AccountName =N'" + account.AccountName + "'").FirstOrDefault();
                    if (data == 1)
                        return Json("Not", JsonRequestBehavior.AllowGet);

                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult CapNhat(Account account)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var data = context.Database.SqlQuery<int>("Select count(*) from Account where AccountName =N'" + account.AccountName + "' AND AccountID !="+account.AccountID).FirstOrDefault();
                    if (data == 1)
                        return Json("Not", JsonRequestBehavior.AllowGet);

                    db.Entry(account).State = EntityState.Modified;
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