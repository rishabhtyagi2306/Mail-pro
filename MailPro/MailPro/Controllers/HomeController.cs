using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.Web.UI.MobileControls;

namespace MailPro.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DataContext Db = new DataContext();


        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentModel obj)
        {
            List<object> Parameters = new List<object>();
            int fid = (int)Session["FacultyID"];
            obj.FacultyID = fid;
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
            Parameters.Add(obj.FacultyID);
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into StudentTable(StudentNo, StudentName, StudentEmail, Branch, Section, Year, IsHosteller, IsCR, StudentCategory, FacultyID) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", objectarray);
            if (output > 0)
            {
                ViewBag.Message = "Data of Student" + obj.StudentName + "is added successfully";
            }
            Session["StudentNo"] = obj.StudentNo;
            int id = (int)Session["CategoryID"];
            ConnectTable CC = new ConnectTable();
            CC.CategoryID = id;
            CC.StudentNo = obj.StudentNo;
            using (var context = new MailProEntities())
            {

                // CC.PrimaryID = 1;
                context.ConnectTable.Add(CC);
                context.SaveChanges();

            }
            return RedirectToAction("Create");
            
        }

        public ActionResult GetAllStudents()
        {
            //StudentTable Stu = new StudentTable();
            int fid = (int)Session["FacultyID"];
            var Data = Db.StudentTables.SqlQuery("Select *From StudentTable where FacultyID ="+ fid).ToList();
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
            return View(productlist);
        }

       /* public ActionResult StudentDetails(int StudentNo)
        {
            var data = Db.StudentTables.SqlQuery("Select *From StudentTable where StudentNo = " + StudentNo).SingleOrDefault();
            return View(data);
        }*/

        public ActionResult Upload(StudentModel obj, FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    //var usersList = new List<StudentModel>();
                    
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var user = new StudentModel();
                            user.StudentNo = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value.ToString());
                            user.StudentName = workSheet.Cells[rowIterator, 2].Value.ToString();
                            user.StudentEmail = workSheet.Cells[rowIterator, 3].Value.ToString();
                            user.Branch = workSheet.Cells[rowIterator, 4].Value.ToString();
                            user.Section = Convert.ToInt32(workSheet.Cells[rowIterator, 5].Value.ToString());
                            user.Year = Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value.ToString());
                            user.IsHosteller = Convert.ToBoolean(Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value.ToString()));
                            user.IsCR = Convert.ToBoolean(Convert.ToInt32(workSheet.Cells[rowIterator, 8].Value.ToString()));
                            user.StudentCategory = workSheet.Cells[rowIterator, 9].Value.ToString();
                            int fid = (int)Session["FacultyID"];
                            user.FacultyID = fid;
                            //usersList.Add(user);
                            List<object> Parameters = new List<object>();
                            obj.StudentNo = user.StudentNo;
                            obj.StudentName = user.StudentName;
                            obj.StudentEmail = user.StudentEmail;
                            obj.Branch = user.Branch;
                            obj.Section = user.Section;
                            obj.Year = user.Year;
                            obj.IsHosteller = user.IsHosteller;
                            obj.IsCR = user.IsCR;
                            obj.StudentCategory = user.StudentCategory;
                            obj.FacultyID = user.FacultyID;

                            Parameters.Add(obj.StudentNo);
                            Parameters.Add(obj.StudentName);
                            Parameters.Add(obj.StudentEmail);
                            Parameters.Add(obj.Branch);
                            Parameters.Add(obj.Section);
                            Parameters.Add(obj.Year);
                            Parameters.Add(obj.IsHosteller);
                            Parameters.Add(obj.IsCR);
                            Parameters.Add(obj.StudentCategory);
                            Parameters.Add(obj.FacultyID);

                            object[] objectarray = Parameters.ToArray();
                            int output = Db.Database.ExecuteSqlCommand("insert into StudentTable(StudentNo, StudentName, StudentEmail, Branch, Section, Year, IsHosteller, IsCR, StudentCategory, FacultyID) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", objectarray);
                            Session["StudentNo"] = obj.StudentNo;
                            int id = (int)Session["CategoryID"];
                            ConnectTable CC = new ConnectTable();
                            CC.CategoryID = id;
                            CC.StudentNo = obj.StudentNo;
                            using (var context = new MailProEntities())
                            {

                                // CC.PrimaryID = 1;
                                context.ConnectTable.Add(CC);
                                context.SaveChanges();

                            }
                        }
                    }
                    
                }
            }
            return View("Create");
        }

    }
}