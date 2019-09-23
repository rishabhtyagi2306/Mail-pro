using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;

namespace MailPro.Controllers
{
    [Authorize]
    public class ConnectController : Controller
    {
        // GET: Connect
        DataContext Db = new DataContext();
        public ActionResult Connection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Connection(ConnectModel obj)
        {
            int cid = (int)Session["CategoryID"];
            int sno = (int)Session["StudentNo"];
            List<object> Parameters = new List<object>();
            obj.CategoryID = cid;
            obj.StudentNo = sno;
            Parameters.Add(obj.CategoryID);
            Parameters.Add(obj.StudentNo);
            object[] objectarray = Parameters.ToArray();

            int output = Db.Database.ExecuteSqlCommand("insert into ConnectTable(CategoryID,StudentNo) values(@p0,@p1)", objectarray);

            return RedirectToAction("Create", "Home");
        }

    }
}