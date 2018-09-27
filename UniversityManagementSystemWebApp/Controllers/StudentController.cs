using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.BusinessLogicLayer;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.UserDefinedModels;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class StudentController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        private UniversityManager universityManager = new UniversityManager();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View(new Student());
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,StudentName,StudentEmail,StudentContactNo,RegistrationDate,Address,DepartmentId,StudentRegistrationNumber")]Student student)
        {
            //  student.StudentRegistrationNumber = universityManager.GetRegistrationNumber(student);

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                ModelState.Clear();

                TempData["message"] = "Successfully Registered";
                // return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }

        public JsonResult IsEmailUnique(string studentEmail)
        {
            bool a = db.Students.ToList().Exists(e => e.StudentEmail == studentEmail);
            bool b = db.Teachers.ToList().Exists(e => e.TeacherEmail == studentEmail);
            return Json(!(a || b), JsonRequestBehavior.AllowGet);
        }

    



        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,StudentName,StudentEmail,StudentContactNo,RegistrationDate,Address,DepartmentId,RegistrationNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetStudentByStudentId(int studentId)
        {
            var data = db.Students.Include(d => d.Department).Select(n => new { StudentId = n.StudentId, StudentName = n.StudentName, StudentEmail = n.StudentEmail, DepartmentName = n.Department.DepartmentName, Courses = n.Department.Courses });
            var student = data.ToList().Find(s => s.StudentId == studentId);
            return Json(student, JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsRegNoUnique(string studentRegistrationNumber)
        {
            bool a = db.Students.ToList().Exists(d => d.StudentRegistrationNumber == studentRegistrationNumber);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsContractNoUnique(string studentContractNo)
        {
            bool a = db.Students.ToList().Exists(d => d.StudentContactNo == studentContractNo);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }
      

    }
}
