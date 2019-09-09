using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace MailPro.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryTable model)
        {

            using (var context = new MailProEntities())
            {


                context.CategoryTable.Add(model);
                context.SaveChanges();
            }

            return View();
        }
    }
}