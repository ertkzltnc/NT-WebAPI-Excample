using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Giris.Models;

namespace WebAPI_Giris.Controllers.api
{
    public class StudentController : ApiController
    {
        SchoolDBEntities ctx = new SchoolDBEntities();
        public IHttpActionResult GetStudentId(int id)
        {
            StudentViewModel student = ctx.Students.Where(x => x.StudentID == id).Select(x=> new StudentViewModel
            {
                StudentID = x.StudentID,
                StudentName = x.StudentName
            }).FirstOrDefault<StudentViewModel>();
            if (student==null)
            {
                return NotFound();
            }
            return Ok(student);

        }

        public IHttpActionResult GetAllStudent()
        {
            List<StudentViewModel> student = ctx.Students.Select(x => new StudentViewModel
            {
                StudentID = x.StudentID,
                StudentName = x.StudentName
            }).ToList<StudentViewModel>();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);

        }
        public IHttpActionResult PostNewStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a vali,d model");
            }
            using (var db=new SchoolDBEntities())
            {
                db.Students.Add(new Student()
                {
                    StudentID = student.StudentID,
                    StudentName = student.StudentName
                });
                db.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult Put(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a vali,d model");
            }
            using (var db = new SchoolDBEntities())
            {
                var existingStudent = db.Students.Where(s => s.StudentID == student.StudentID).FirstOrDefault<Student>();
                if (existingStudent !=null)
                {
                    existingStudent.StudentName = student.StudentName;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

    }
}
