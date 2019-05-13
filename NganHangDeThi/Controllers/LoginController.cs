using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;

namespace NganHangDeThi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string name, string pass)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                var data = context.Accounts.Where(x => x.AccountName == name).Where(x => x.Pass == pass).Where(x => x.Status == true).FirstOrDefault();
                if(data != null)
                {
                    Session["ID"] = context.Database.SqlQuery<int>("Select AccountID from Account where AccountName= '" + name + "' and Pass='" + pass + "'").FirstOrDefault();
                    Session["Name"] = context.Database.SqlQuery<string>("Select AccountName from Account where AccountName= '" + name + "' and Pass='" + pass + "'").FirstOrDefault();
                    var Decentralization = context.Database.SqlQuery<int>("Select Decentralization from Account where AccountName= '" + name + "' and Pass='" + pass + "'").FirstOrDefault();
                    Session["Q"] = Decentralization;
                    return Json(Decentralization, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}