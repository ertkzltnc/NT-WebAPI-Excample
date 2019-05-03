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
    }
}
