using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using MailPro.Models;
using System.IO;
using System.Threading.Tasks;

namespace MailPro.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        DataContext Db = new DataContext();
        
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Mail()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ViewResult> Mail(Mails model)
        {
           
            try
            {
                using (var context = new MailProEntities())
                {
                    
                    int Fac = (int)Session["FacultyID"];
                    model.FacultyID = Fac;
                    List<int> fetch = (List<int>)Session["MailTransfer"];
                    StudentTable st = new StudentTable();
                    int i = 0;
                    int count = 0;
                    foreach (var item in fetch)
                    {
                        st = context.StudentTable.SingleOrDefault(x => x.StudentNo == item);
                        ViewBag.Mailer += st.StudentEmail + ",<br/>";
                        i = i + 1;
                        if(i<=2)
                        {
                            ViewBag.MailPreview += st.StudentEmail + ',';
                        }
                        else
                        {
                            count++;
                        }
                    }
                    ViewBag.MailPreview += "and " + count + " others";
                    model.Sent = ViewBag.Mailer;
                    model.MailPreview = ViewBag.MailPreview;
                    await EmailSent(model);
                    if (ViewBag.Message != null)
                    {

                    }
                    model.GmailPassword = "unknown";
                    context.Mails.Add(model);
                    context.SaveChanges();

                }
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "");
                //ViewBag.Msg = "It seems like you haven't selected your recipients";
                return View();
            }
        }



        [Authorize]
        public ActionResult DeleteTemplate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteTemplate(int TemplateID)
        {
            var productlist = Db.Database.ExecuteSqlCommand("delete from TemplateTable where TemplateID=@p0", TemplateID);

            if (productlist != 0)
            {
                return RedirectToAction("ShowTemplate");
            }
            return View(productlist);
        }

        [Authorize]
        public ActionResult CreateTemplate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTemplate(FormCollection formCollection, TemplateModel obj)
        {
            HttpPostedFileBase postedFile = Request.Files["postedFile"];
            string filename = Path.GetFileName(postedFile.FileName);
            string TempPath = "/Templates/" + filename;
            postedFile.SaveAs(Server.MapPath(TempPath));
            obj.TemplateURL = TempPath;
            obj.TemplateName = filename;
            obj.TemplateImage = "../TemplatesImage/NotAvailable.jpg";

            List<object> Parameters = new List<object>();

            Parameters.Add(obj.TemplateURL);
            Parameters.Add(obj.TemplateName);
            Parameters.Add(obj.TemplateImage);
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into TemplateTable(TemplateURL, TemplateName, TemplateImage) values(@p0,@p1,@p2)", objectarray);
            return RedirectToAction("CreateTemplate");
        }


        [Authorize]
        public ActionResult ShowTemplate()
        {
            var Data = Db.TemplateTables.SqlQuery("Select *From TemplateTable").ToList();
            return View(Data);
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task EmailSent(Mails model)
        {
            List<int> fetch = (List<int>)Session["MailTransfer"];

            int Fac = (int)Session["FacultyID"];
            FacultyTable ft = new FacultyTable();
            var context = new MailProEntities();
            ft = context.FacultyTable.Find(Fac);
            try
            {
                StudentTable st = new StudentTable();
                foreach (var item in fetch)
                {
                    st = context.StudentTable.SingleOrDefault(x => x.StudentNo == item);
                    await sendprocess(st, ft, model);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "There was some error in sending mail , Recheck your password and internet connection");
            }
            

            
        }

        public async Task sendprocess(StudentTable st, FacultyTable ft, Mails model)
        {

            var FromEmail = new MailAddress(ft.FacultyEmail, ft.FacultyName);
            var ToEmail = new MailAddress(st.StudentEmail);
            var FromEmailPassword = model.GmailPassword;

            string URL = Session["TemplateUrl"].ToString();
            string Subject = model.Subject;
            string Body = "Hello " + st.StudentName + ",<br/><br/>" + model.Contents;
            Body = PopulateBody(Body, URL);

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

                await smtp.SendMailAsync(message);
            ViewBag.Message = "Your Mail Has been sent successfully";




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

        [Authorize]
        public ActionResult ShowSentEmail()
        {
            int fac = (int)Session["FacultyID"];
            var Data = Db.GetMails.SqlQuery("Select *From Mails where FacultyID ="+ fac).ToList();
            return View(Data);
        }

        [Authorize]
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
                return RedirectToAction("ShowSentEmail");
            }
            return RedirectToAction("ShowSentMail");
        }

        public ActionResult EmailDetail(int MailID)
        {
            var data = Db.GetMails.SqlQuery("Select *From Mails where MailID = " + MailID).SingleOrDefault();
            return View(data);
        }
    }
}