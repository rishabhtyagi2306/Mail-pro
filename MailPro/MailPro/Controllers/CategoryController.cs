﻿using System;
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
        public ActionResult AddCategory(CategoryModel obj, FacultyTable FacultyID)
        {
            List<object> Parameters = new List<object>();
            //FacultyTable st = new FacultyTable();

            Parameters.Add(obj.CategoryName);
            Parameters.Add(obj.FacultyID);
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into CategoryTable(CategoryName, FacultyID) values(@p0,@p1)", objectarray);
            {
                ViewBag.Message = "Data of Student" + obj.CategoryName + "is added successfully";
            }
            return RedirectToAction("Create");
        }
    }
}