using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;

namespace MailPro.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        
        DataContext Db = new DataContext();
        
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel obj)
        {
            int fid = (int)Session["FacultyID"];
            List<object> Parameters = new List<object>();
            obj.FacultyID = fid;

            Parameters.Add(obj.CategoryName);
            Parameters.Add(obj.FacultyID);
            //string CName = obj.CategoryName;
            object[] objectarray = Parameters.ToArray();
            int output = Db.Database.ExecuteSqlCommand("insert into CategoryTable(CategoryName,FacultyID) values(@p0,@p1)", objectarray);
            CategoryTable c = new CategoryTable();
            using (var context = new MailProEntities())
            {
                var s = obj.CategoryName;

                c = context.CategoryTable.SingleOrDefault(x => x.CategoryName == s);
                Session["CategoryID"] = c.CategoryID;
            }

            return RedirectToAction("AddCategory");
        }

        public ActionResult ShowCategory()
        {
            int fid = (int)Session["FacultyID"];
            var Data = Db.CategoryTables.SqlQuery("Select *From CategoryTable where FacultyID ="+ fid).ToList();
            return View(Data);
        }

        public ActionResult AddStudentToCategory(CategoryModel model)
        {
            CategoryTable c = new CategoryTable();
            Session["CategoryID"] = model.CategoryID;
            return RedirectToAction("Create", "Home", new { area = ""});
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

        List<StudentModel> student = new List<StudentModel>();
        List<CategoryModel> AdditionalInfo = new List<CategoryModel>();
        List<ConnectModel> info = new List<ConnectModel>();
        List<Membership> info1 = new List<Membership>();


        public ActionResult show()
        {
            int fid = (int)Session["FacultyID"];
            var context = new MailProEntities();
            InsertData();
            InsertData1();
            InsertData2();
            InsertData3();
            Temp vr = new Temp();

            var studentViewModel = from a in student
                                   join e in info on a.StudentNo equals e.StudentNo
                                   join t in AdditionalInfo on e.CategoryID equals t.CategoryID
                                   join h in info1 on t.FacultyID equals h.FacultyID
                                   where h.FacultyID == fid
                                   select new ViewModel { studentmodelvm = a, categorymodelvm = t };

            List<string> mails = new List<string>();
            List<Class1> data = new List<Class1>();
            foreach (var item in studentViewModel)
            {
                mails.Add(@item.categorymodelvm.CategoryName);
                data.Add(new Class1
                {
                    StudentCategory = @item.studentmodelvm.StudentCategory,
                    StudentEmail = @item.studentmodelvm.StudentEmail,
                    Section = item.studentmodelvm.Section,
                    CategoryName = @item.categorymodelvm.CategoryName,
                    CategoryID = @item.categorymodelvm.CategoryID,
                    FacultyID = @item.categorymodelvm.FacultyID,
                    StudentName = @item.studentmodelvm.StudentName,
                    StuidentNo = @item.studentmodelvm.StudentNo,
                    Branch = @item.studentmodelvm.Branch,
                    IsHosteller = @item.studentmodelvm.IsHosteller,
                    IsCR = @item.studentmodelvm.IsCR,
                    IsSelected = false,



                });

            }
            mails = mails.Distinct().ToList();
            Session["mails"] = mails;
            Session["data"] = data;


            int output = Db.Database.ExecuteSqlCommand("delete from Temp");

            foreach (var item in studentViewModel)
            {
                vr.StudentName = @item.studentmodelvm.StudentName;
                vr.StudentEmail = @item.studentmodelvm.StudentEmail;
                vr.StudentCategory = @item.studentmodelvm.StudentCategory;
                vr.StuidentNo = @item.studentmodelvm.StudentNo;
                vr.Year = @item.studentmodelvm.Year;
                vr.Section = @item.studentmodelvm.Section;
                vr.IsHosteller = @item.studentmodelvm.IsHosteller;
                vr.IsCR = @item.studentmodelvm.IsCR;
                vr.CategoryID = @item.categorymodelvm.CategoryID;
                vr.Branch = @item.studentmodelvm.Branch;
                vr.CategoryName = @item.categorymodelvm.CategoryName;
                vr.FacultyID = @item.categorymodelvm.FacultyID;
                context.Temp.Add(vr);
                context.SaveChanges();

            }


            return RedirectToAction("Displays");
            //ViewModel varr = new ViewModel();
            //new ViewModel
            //{


            //};
            //return View(studentViewModel);


        }

        public ActionResult Displays()
        {
            DisplayClass fruit = new DisplayClass();
            fruit.StudentNames = PopulateNames();
            return View(fruit);
        }
        [HttpPost]
        public ActionResult Displays(DisplayClass fruit)
        {
            fruit.StudentNames = PopulateNames();
            if (fruit.ids != null)
            {
                List<SelectListItem> selectedItems = fruit.StudentNames.Where(p => fruit.ids.Contains(int.Parse(p.Value))).ToList();
                List<int> StudentNoMail = new List<int>();
                ViewBag.Message = "Selected Fruits:";
                foreach (var selectedItem in selectedItems)
                {
                    var intno = Convert.ToInt32(selectedItem.Value);
                    selectedItem.Selected = true;
                    ViewBag.Message += "\\n" + selectedItem.Text;
                    StudentNoMail.Add(intno);
                }
                Session["MailTransfer"] = StudentNoMail;
            }

            return RedirectToAction("Mail", "Email");
        }
        private static List<SelectListItem> PopulateNames()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Class1> obj = new List<Class1>();
            List<Temp> data = new List<Temp>();
            //string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            string strConnection = "Data Source =.; Initial Catalog = MailPro; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(strConnection))
            {
                string query = " SELECT StuidentNo,StudentName,CategoryName from Temp";
                // List<List> sbc = new List<List>();
                using (SqlCommand cmdd = new SqlCommand(query))
                {
                    cmdd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmdd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["StudentName"].ToString(),
                                Value = sdr["StuidentNo"].ToString(),

                            });

                        }
                    }
                    con.Close();
                }


                return items;
            }
        }
        public void InsertData()
        {


            int fid = (int)Session["FacultyID"];

            // string strConnection = "Data Source=.; uid=sa; pwd=wintellect;database=Genericdatabase;";

            string strConnection = "Data Source =.; Initial Catalog = MailPro; Integrated Security = True";
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(strConnection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * from StudentTable");
            cmd.Connection = con;
            // SqlConnection conn = new SqlConnection("strconnection");

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
                student.Add(new StudentModel
                {
                    StudentNo = (int)dt.Rows[i]["StudentNo"],
                    StudentName = dt.Rows[i]["StudentName"].ToString(),
                    StudentEmail = dt.Rows[i]["StudentEmail"].ToString(),
                    Branch = dt.Rows[i]["Branch"].ToString(),
                    Section = (int)dt.Rows[i]["Section"],
                    Year = (int)dt.Rows[i]["Year"],
                    IsHosteller = (bool)dt.Rows[i]["IsHosteller"],
                    IsCR = (bool)dt.Rows[i]["IsCR"],
                    StudentCategory = dt.Rows[i]["StudentCategory"].ToString(),


                });
        }

        public void InsertData1()
        {
            //using (var context = new MailProEntities())
            //{
            //    var abc= from st in context.StudentTable
            //               select st;
            //           return View();
            //}


            int fid = (int)Session["FacultyID"];

            // string strConnection = "Data Source=.; uid=sa; pwd=wintellect;database=Genericdatabase;";

            string strConnection = "Data Source =.; Initial Catalog = MailPro; Integrated Security = True";

            SqlConnection con = new SqlConnection(strConnection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * from CategoryTable");
            cmd.Connection = con;
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
                AdditionalInfo.Add(new CategoryModel
                {
                    CategoryID = (int)dt.Rows[i]["CategoryID"],
                    CategoryName = dt.Rows[i]["CategoryName"].ToString(),
                    FacultyID = (int)dt.Rows[i]["FacultyID"],


                });



        }
        public void InsertData2()
        {



            int fid = (int)Session["FacultyID"];

            // string strConnection = "Data Source=.; uid=sa; pwd=wintellect;database=Genericdatabase;";

            string strConnection = "Data Source =.; Initial Catalog = MailPro; Integrated Security = True";
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(strConnection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * from ConnectTable");
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
                info.Add(new ConnectModel
                {
                    CategoryID = (int)dt.Rows[i]["CategoryID"],
                    StudentNo = (int)dt.Rows[i]["StudentNo"],
                    PrimaryID = (int)dt.Rows[i]["PrimaryID"],


                });



        }
        public void InsertData3()
        {

            int fid = (int)Session["FacultyID"];

            // string strConnection = "Data Source=.; uid=sa; pwd=wintellect;database=Genericdatabase;";

            string strConnection = "Data Source =.; Initial Catalog = MailPro; Integrated Security = True";
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(strConnection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * from FacultyTable");
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
                info1.Add(new Membership
                {
                    FacultyName = dt.Rows[i]["FacultyName"].ToString(),
                    FacultyEmail = dt.Rows[i]["FacultyEmail"].ToString(),
                    FacultyPhoneNo = dt.Rows[i]["FacultyPhoneNo"].ToString(),
                    Department = dt.Rows[i]["Department"].ToString(),
                    Password = dt.Rows[i]["Password"].ToString(),
                    FacultyID = (int)dt.Rows[i]["FacultyID"],


                });



        }

    }
}