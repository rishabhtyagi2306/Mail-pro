using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using MailPro.Models;
using System.IO;

namespace MailPro.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        DataContext Db = new DataContext();
        // GET: Email
        [Authorize]
        public ActionResult Mail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mail(Mails model)
        {
           
            using (var context = new MailProEntities())
            {
                //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
                //FacultyTable Fac = new FacultyTable();

                //Fac.Password = Crypto.Hash(Fac.Password);
                int Fac = (int)Session["FacultyID"];
                model.FacultyID = Fac;
                context.Mails.Add(model);
                context.SaveChanges();
                EmailSent(model);
            }
            return View();
        }

        public ActionResult CreateTemplate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTemplate(TemplateModel obj)
        {
            List<object> Parameters = new List<object>();
            
            Parameters.Add(obj.TemplateURL);
            Parameters.Add(obj.TemplateName);
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into TemplateTable(TemplateURL, TemplateName) values(@p0,@p1)", objectarray);
            return RedirectToAction("CreateTemplate");
        }
        public ActionResult ShowTemplate()
        {
            var Data = Db.TemplateTables.SqlQuery("Select *From TemplateTable").ToList();
            return View(Data);
        }

        public ActionResult SelectTemplate(TemplateModel model)
        {
           using (var context = new MailProEntities())
            {
                TemplateTable tt = new TemplateTable();
                tt = context.TemplateTable.SingleOrDefault(x => x.TemplateID == model.TemplateID);
                string y = tt.TemplateURL;
                Session["TemplateURL"] = y;
            }
            return RedirectToAction("Show", "Category");
        }

        
        [HttpGet]
        public void EmailSent(Mails model)
        {
            List<int> fetch = (List<int>)Session["MailTransfer"];

            int Fac = (int)Session["FacultyID"];
            FacultyTable ft = new FacultyTable();
            var context = new MailProEntities();
            ft = context.FacultyTable.Find(Fac);
            StudentTable st = new StudentTable();
            foreach (var item in fetch)
            {
                st = context.StudentTable.SingleOrDefault(x => x.StudentNo == item);
                var FromEmail = new MailAddress(ft.FacultyEmail, ft.FacultyName);
                var ToEmail = new MailAddress(st.StudentEmail);
                var FromEmailPassword = model.GmailPassword;
                
                //string URL = Session["TemplateUrl"].ToString();
                string Subject = model.Subject;
                string Body = "Hello " + st.StudentName + ",<br/><br/>" + model.Contents;
                //Body = PopulateBody(Body,URL);
               
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(FromEmail.Address, FromEmailPassword)
                };
                /*model.GmailPassword = Crypto.Hash(model.GmailPassword);
                context.Mails.Add(model);
                context.SaveChanges();*/
                using (var message = new MailMessage(FromEmail, ToEmail)
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                })

                    smtp.Send(message);
            }

            
        }

        public string PopulateBody(string contents, string URL)
        {
            string Body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(URL)))
            {
                Body = reader.ReadToEnd();
            }
            Body = Body.Replace("{Body}", contents);
            return Body;
        }

        public ActionResult ShowSentEmail()
        {
            int fac = (int)Session["FacultyID"];
            var Data = Db.GetMails.SqlQuery("Select *From Mails where FacultyID ="+ fac).ToList();
            return View(Data);
        }

        public ActionResult DeleteEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteEmail(int MailID)
        {
            var productlist = Db.Database.ExecuteSqlCommand("delete from Mails where MailID = @p0", MailID);

            if (productlist != 0)
            {
                return RedirectToAction("ShowSentEmails");
            }
            return View(productlist);
        }

        public ActionResult EmailDetail(int MailID)
        {
            var data = Db.GetMails.SqlQuery("Select *From Mails where MailID = " + MailID).SingleOrDefault();
            return View(data);
        }
    }
}