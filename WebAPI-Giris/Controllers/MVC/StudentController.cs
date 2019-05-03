using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPI_Giris.Models;

namespace WebAPI_Giris.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<StudentViewModel> student = null;
            using (var client=new HttpClient())
            {
                 client.BaseAddress = new Uri("http://localhost:50653/api/");
                //client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/");
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));

                //HTTP GET 
                var responseTask = client.GetAsync("student?id=1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
                    readTask.Wait();

                    student = readTask.Result;
                }
                else
                {
                    student = Enumerable.Empty<StudentViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }
            }
            return View(student);

        }
    }
}