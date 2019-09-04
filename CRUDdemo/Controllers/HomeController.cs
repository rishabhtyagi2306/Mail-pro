using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoModel.Model;
using demoDB.Db.DbOperations;

namespace CRUDdemo.Controllers
{
    public class HomeController : Controller
    {
        StudentsRepository repository = null;

        public HomeController()
        {
            repository = new StudentsRepository();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(StudentsModel model)
        {
            if (ModelState.IsValid)
            {
                int StudentNo = repository.AddStudent(model);
                if(StudentNo > 0)
                {
                    ModelState.Clear();
                    ViewBag.Issuccess = "Data Added";
                }
            }
            return View();
        }

        public ActionResult GetAllStudents()
        {
            var result = repository.GetAllStudents();
            return View(result);
        }

        public ActionResult Details(int StudentNo)
        {
            var Students = repository.GetStudent(StudentNo);
            return View(Students);
        }

        //[Route("home/edit/{id}")]
        public ActionResult Edit(int StudentNo)
        {
            var Students = repository.GetStudent(StudentNo);
            return View(Students);
        }

        [HttpPost]
        public ActionResult Edit(StudentsModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateStudents(model.StudentNo, model);
                return RedirectToAction("GetAllStudents");
            }
            return View();
        }

        
        public ActionResult Delete(StudentsModel model)
        {
            repository.DeleteStudent(model.StudentNo);
            return RedirectToAction("GetAllStudents");
        }

    }
}