using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;
using NganHangDeThi.Models;

namespace NganHangDeThi.Controllers
{
    public class TestingController : TestLoginController
    {
        private readonly NganHangDeThiEntities1 db = new NganHangDeThiEntities1();
        // GET: Testing
        public ActionResult Index(int id, int level)
        {
            try
            {
                using (var context = new NganHangDeThiEntities1())
                {
                    ViewBag.Class = context.Classes.Find(id);
                    
                   
                    if (level == 1)
                        ViewBag.ListExam = context.Exams.Where(x => x.Class == id).OrderBy(x => x.Level).OrderBy(x => Guid.NewGuid()).Take(40).ToList();
                    if (level == 2)
                        ViewBag.ListExam = context.Exams.Where(x => x.Class == id).Where(x => x.Level != '3').OrderByDescending(x => x.Level).OrderBy(x => Guid.NewGuid()).Take(40).ToList();
                    if (level == 3)
                        ViewBag.ListExam = context.Exams.Where(x => x.Class == id).OrderByDescending(x => x.Level).OrderBy(x => Guid.NewGuid()).Take(40).ToList();
                    ViewBag.Time = 60;
                    ViewBag.level = level;
                    ViewBag.cl = id;
                }
                return View();
            }
            catch (Exception ex )
            {

                throw;
            }
            
        }
        struct DIEMS
        {
            public List<TestResults> ds;
            public int sl;
        }
        public ActionResult Point(List<TestResults> model, int cl,int lv, float time)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                try
                {
                    int t = 0;
                    foreach(var i in model)
                    {
                        var data = context.Exams.Where(x => x.ExamID == i.ExamID).Where(x => x.True == i.AnswerTrue).FirstOrDefault();
                        if (data != null)
                            t++;
                        else
                        {
                            i.AnswerTrue = context.Database.SqlQuery<string>("Select True from Exam where ExamID =" + i.ExamID).FirstOrDefault();
                        }
                    }
                    List<DIEMS> DIEM = new List<DIEMS>();
                    DIEMS diem1 = new DIEMS();
                    diem1.ds = model;
                    diem1.sl = t;
                    DIEM.Add(diem1);
                    TestScore score = new TestScore();
                    score.AccountID = Convert.ToInt32(Session["ID"]);
                    score.Test = t;
                    score.DateTest = DateTime.Now;
                    score.Class = cl;
                    score.Level = lv;
                    score.Time = time;
                    db.TestScores.Add(score);
                    db.SaveChanges();

                    return Json(DIEM, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {

                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                
            }
                
        }
    }
}