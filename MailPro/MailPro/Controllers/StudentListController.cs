using MailPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MailPro.Controllers
{
    public class StudentListController : Controller
    {
        // GET: StudentList
        DataContext Db = new DataContext(); 
        public ActionResult StudentList()
        {
            StudentModel Stud = new StudentModel();
            using (var context = new MailProEntities())
            {
                Stud.StudentTables = Db.StudentTables.ToList<StudentTable>();
            }
            return View(Stud);
        }

        [HttpPost]
        public ActionResult StudentList(StudentModel model)
        {
            StudentModel pass = new StudentModel();
            foreach (var item in model.StudentEmail)
            {
                pass.StudentEmail = model.StudentEmail;
            }
            Session["passEmail"] = pass.StudentEmail;
            return View();
        }
    }
}