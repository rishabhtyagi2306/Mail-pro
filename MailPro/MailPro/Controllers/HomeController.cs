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
            if (output > 0)
            {
                ViewBag.Message = "Data of Student" + obj.StudentName + "is added successfully";
            }
            return RedirectToAction("Create");
        }

        public ActionResult GetAllStudents()
        {
            //StudentTable Stu = new StudentTable();
            var Data = Db.StudentTables.SqlQuery("Select *From StudentTable").ToList();
            return View(Data);
        }

        public ActionResult Edit()
        {
            //var Data = Db.StudentTables.SqlQuery("Select *From StudentTable where StudentNo = @p0");
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int StudentNo, StudentModel obj)
        {
            List<object> Parameters = new List<object>();
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
            int Output = Db.Database.ExecuteSqlCommand("Update StudentTable set StudentNo = @p0, StudentName = @p1, StudentEmail = @p2, Branch = @p3, Section = @p4, Year = @p5, IsHosteller = @p6, IsCR = @p7, StudentCategory = @p8 where StudentNo = @p0", objectarray);

            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(int StudentNo)
        {
            var Data = Db.StudentTables.SqlQuery("Select *From StudentTable where StudentNo = @p0", StudentNo).SingleOrDefault();
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int StudentNo)
        {
            var productlist = Db.Database.ExecuteSqlCommand("delete from StudentTable where StudentNo=@p0", StudentNo);

            if (productlist != 0)
            {
                return RedirectToAction("GetAllStudents");
            }
            return View();
        }

    }
}