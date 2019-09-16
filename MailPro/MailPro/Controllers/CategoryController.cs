using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;

namespace MailPro.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DataContext Db = new DataContext();
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel obj)
        {
            //Session["FacultyID"] = FacultyID;
            int fid = (int)Session["FacultyID"];
            List<object> Parameters = new List<object>();
            //var fac = (String)Session["FacultyID"];
            //CategoryTable ct = new CategoryTable();
            obj.FacultyID = fid;

            Parameters.Add(obj.CategoryName);
            Parameters.Add(obj.FacultyID);
            //string CName = obj.CategoryName;
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into CategoryTable(CategoryName,FacultyID) values(@p0,@p1)", objectarray);
            // CategoryModel Cm = new CategoryModel();
           // var Cm = Db.CategoryTables.SqlQuery("Select CategoryID from CategoryTable where CategoryName = @p0");

            //Session["FacID"] = fid; 
          //  Session["CategoryID"] = Cm.CategoryID;

            return RedirectToAction("AddCategory");
        }

        public ActionResult ShowCategory()
        {
            //List<object> Parameters = new List<object>();
            int fid = (int)Session["FacultyID"];
            //obj.FacultyID = (int)Session["FacID"];
            //Parameters.Add(obj.FacultyID);
            //object[] objectarray = Parameters.ToArray();
            var Data = Db.CategoryTables.SqlQuery("Select *From CategoryTable where FacultyID ="+ fid).ToList();
            return View(Data);
        }

        public ActionResult DeleteCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCategory(int CategoryID)
        {
            var productlist = Db.Database.ExecuteSqlCommand("delete from CategoryTable where CategoryID = " + CategoryID);

            if (productlist != 0)
            {
                return RedirectToAction("ShowCategory");
            }
            return RedirectToAction("ShowCategory");
        }

    }
}