using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NganHangDeThi.Controllers
{
    public class ReportIDController : TestLoginController
    {
        // GET: ReportID
        public ActionResult Index()
        {
            using (var context = new NganHangDeThiEntities1())
            {
                int id = Convert.ToInt32(Session["ID"]);
                ViewBag.Class = context.Database.SqlQuery<Class>("SELECT * FROM dbo.Class c WHERE c.ClassID IN(SELECT Class FROM dbo.TestScore WHERE AccountID = "+id+")").ToList();
                
                return View();
            }

        }

        public ActionResult DrawChart1( int cl)
        {
            using (var context = new NganHangDeThiEntities1())
            {
                int id = Convert.ToInt32(Session["ID"]);
                var data = context.TestScores.Where(x => x.AccountID == id).Where(x => x.Class == cl).ToList();
                DataTable dt = ConvertToDataTable(data);
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                return Json(new { jsonData = jsonString });
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    }
}