using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult Register(string name, string email, string pass)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    var id = context.Database.SqlQuery<int>("Select count(*) from Account where AccountName =N'" + name + "'").FirstOrDefault();
                    var em = context.Database.SqlQuery<int>("Select count(*) from Account where Email =N'" + email + "'").FirstOrDefault();
                    if (id == 1)
                    {
                        return Json("Account", JsonRequestBehavior.AllowGet);
                    }
                    if(em == 1)
                    {
                        return Json("Email", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
                        Account acc = new Account();
                        acc.AccountName = name;
                        acc.Email = email;
                        acc.Pass = pass;
                        acc.CreateDate = DateTime.Now;
                        acc.Status = true;
                        acc.Decentralization = 2;

                        db.Accounts.Add(acc);
                        db.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet); ;
                    }
                }
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }
    }
}