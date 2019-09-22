using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace MailPro.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email

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
                List<int> fetch = (List<int>)Session["MailTransfer"];
                List<string> mailer = new List<string>();
                StudentTable st = new StudentTable();
                
                foreach (var item in fetch)
                {
                    st = context.StudentTable.SingleOrDefault(x => x.StudentNo == item);
                    ViewBag.mail +=","+ st.StudentEmail;
                    

                }
                model.Sent = ViewBag.mail;
                context.Mails.Add(model);
                context.SaveChanges();
                EmailSent(model);
            }
            return View();
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
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(ft.FacultyEmail, ft.FacultyName);
                    //mail.To.Add(model.Sent);
                    //mail.To.Add(model.Sent);
                }

                string Subject = model.Subject;
                string Body = "Hello"+st.StudentName+model.Contents;

                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(FromEmail.Address, FromEmailPassword)
                };

                using (var message = new MailMessage(FromEmail, ToEmail)
                {
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true
                })

                    smtp.Send(message);
            }
        } 

        public ActionResult ShowMails()
        {
            int fid = (int)Session["FacultyID"];
            var context=new MailProEntities();
            var s = context.Mails.SqlQuery("Select * from Mails where FacultyID=" + fid).ToList();
            return View(s);
        }
    }
}


