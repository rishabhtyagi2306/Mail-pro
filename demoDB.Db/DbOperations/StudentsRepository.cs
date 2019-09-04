using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demoModel.Model;
using System.Web;



namespace demoDB.Db.DbOperations
{
    public class StudentsRepository
    {
        public int AddStudent(StudentsModel model)
        {
            using (var context = new demoCRUDEntities())
            {
                Student stu = new Student()
                {
                    Name = model.Name,
                    Branch = model.Branch,
                    Section = model.Section,
                    StudentNo = model.StudentNo,
                    EmailId = model.EmailId
                };

                context.Student.Add(stu);

                context.SaveChanges();

                return stu.StudentNo;
            }

        }
        public List<StudentsModel> GetAllStudents()
        {
            using (var context = new demoCRUDEntities())
            {
                var result = context.Student
                    .Select(x => new StudentsModel()
                    {
                        Name = x.Name,
                        Branch = x.Branch,
                        StudentNo = x.StudentNo,
                        Section = x.Section,
                        EmailId = x.EmailId
                    }).ToList();

                return result;
            }
        }
        
        public StudentsModel GetStudent(int StudentNo)
        {
            using (var context = new demoCRUDEntities())
            {
                var result = context.Student
                    .Where(x => x.StudentNo == StudentNo)
                    .Select(x => new StudentsModel()
                    {
                        Name = x.Name,
                        Branch = x.Branch,
                        StudentNo = x.StudentNo,
                        Section = x.Section,
                        EmailId = x.EmailId
                    }).FirstOrDefault();

                return result;
            }
        }

        public bool UpdateStudents(int StudentNo, StudentsModel model)
        {
            using (var context = new demoCRUDEntities())
            {
                var student = context.Student.FirstOrDefault(x => x.StudentNo == StudentNo);

                if(student != null)
                {
                    student.Section = model.Section;
                    student.Branch = model.Branch;
                    student.EmailId = model.EmailId;
                    student.Name = model.Name;
                }

                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteStudent(int studentNo)
        {
            using (var context = new demoCRUDEntities())
            {
                var student = context.Student.FirstOrDefault(x => x.StudentNo == studentNo);
                if(student != null)
                {
                    context.Student.Remove(student);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
