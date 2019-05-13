using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;
using System.Data;
using System.ComponentModel;

namespace NganHangDeThi.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Admin/Report
        public ActionResult Index()
        {
            using(var context = new NganHangDeThiEntities1())
            {
                ViewBag.listAccounts = context.Accounts.Where(x =>x.Decentralization == 2).ToList();
                ViewBag.listClass = context.Classes.ToList();
            }
            return View();
        }

        public ActionResult DrawChart(int id, int cl)
        {
            using (var context = new NganHangDeThiEntities1())
            {
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