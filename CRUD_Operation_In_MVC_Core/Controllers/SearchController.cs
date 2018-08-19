using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Operation_In_MVC_Core.Model.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_Operation_In_MVC_Core.Controllers
{
    public class SearchController : Controller
    {
        private mydbContext db = new mydbContext();
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult StudentExamFee()
        {
            ViewBag.studentid = new SelectList(db.Student, "id", "name");

            ViewBag.students = new List<Student>();
            ViewBag.results = new List<Examresult>();
            ViewBag.fees = new List<Fee>();
            return View();
        }
        [HttpPost, ActionName("StudentExamFee")]
        public ActionResult StudentExamFee(int studentid = 0)
        {
            try
            {
                ViewBag.studentid = new SelectList(db.Student, "id", "name");
                ViewBag.students = (from st in db.Student select new { st.Id, st.Name });
                List<Student> studentList = (from st in db.Student where st.Id == studentid select st).ToList();
                ViewBag.students = studentList;
                List<Examresult> res = (from st in db.Examresult where st.Studentid == studentid orderby st.Examsl select st).ToList();
                ViewBag.results = res;
                List<Fee> fees = (from st in db.Fee join s in db.Student on st.StudentId equals s.Id where st.StudentId == studentid orderby st.Voucherno select st).ToList();
                ViewBag.fees = fees;
                ViewBag.Message = "Post Worked";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}