using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;
using System.Data.SqlClient;

namespace MailPro.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DataContext Db = new DataContext();
        //[Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentModel obj)
        {
            List<object> Parameters = new List<object>();
            //StudentTable st = new StudentTable();

            Parameters.Add(obj.StudentNo);
            Parameters.Add(obj.StudentName);
            Parameters.Add(obj.StudentEmail);
            Parameters.Add(obj.Branch);
            Parameters.Add(obj.Section);
            Parameters.Add(obj.Year);
            Parameters.Add(obj.IsHosteller);
            Parameters.Add(obj.IsCR);
            Parameters.Add(obj.StudentCategory);
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into StudentTable(StudentNo, StudentName, StudentEmail, Branch, Section, Year, IsHosteller, IsCR, StudentCategory) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", objectarray);
            if(output > 0)
            {
                ViewBag.Message = "Data of Student" + obj.StudentName + "is added successfully";
            }
            return RedirectToAction("Create");
        }
    }
}