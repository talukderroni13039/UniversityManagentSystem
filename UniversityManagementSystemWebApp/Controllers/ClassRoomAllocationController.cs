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
    public class ClassRoomAllocationController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
       // UniversityManager universityManager = new UniversityManager();

        // GET: ClassRoomAllocation
        public ActionResult Index(int departmentId = 0)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", departmentId);
            var classRoomAllocations = db.ClassRoomAllocations.Include(c => c.Course).Include(c => c.Department).Include(c => c.Room).ToList().FindAll(c => (c.DepartmentId == departmentId) && (c.IsAllocated == true) );
            if (departmentId != 0)
            {
                var courses = db.Courses.ToList().FindAll(c => c.DepartmentId == departmentId);
                foreach (var course in courses)
                {
                    if (!classRoomAllocations.Exists(c => c.CourseId == course.CourseId))
                    {
                        ClassRoomAllocation aClassRoomAllocation = new ClassRoomAllocation();
                        aClassRoomAllocation.Course = course;

                        classRoomAllocations.Add(aClassRoomAllocation);
                    }
                }
                return View(classRoomAllocations);
            }
            return View(classRoomAllocations);
        }


        // GET: ClassRoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            if (classRoomAllocation == null)
            {
                return HttpNotFound();
            }
            return View(classRoomAllocation);
        }

        // GET: ClassRoomAllocation/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList("", "Select....");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo");
            return View();
        }

        // POST: ClassRoomAllocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllocationId,DepartmentId,CourseId,RoomId,Day,StartTime,EndTime")] ClassRoomAllocation classRoomAllocation)
        {
            if (ModelState.IsValid)
            {
                if (AllocateClassRoom(classRoomAllocation))
                {
                    classRoomAllocation.IsAllocated = true;
                    db.ClassRoomAllocations.Add(classRoomAllocation);
                    db.SaveChanges();
                    ModelState.Clear();

                    TempData["message"] = " ClassRoom Successfully Allocated";


                    //return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Allocation = "Allocation is not possible";
                }
            }

            ViewBag.CourseId = new SelectList("", "Select....");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo");
            return View();
        }

        private bool AllocateClassRoom(ClassRoomAllocation classRoomAllocation)
        {
            var allocations = db.ClassRoomAllocations.ToList().FindAll(l => l.RoomId == classRoomAllocation.RoomId && l.Day == classRoomAllocation.Day && l.IsAllocated == true);
            bool possible = true;

            foreach (var allocation in allocations)
            {
                var ps = allocation.StartTime;
                var pe = allocation.EndTime;

                if ((classRoomAllocation.StartTime <= ps && classRoomAllocation.EndTime <= ps) || (classRoomAllocation.StartTime >= pe && classRoomAllocation.EndTime >= pe))
                {
                    possible = true;
                }

                else
                {
                    possible = false;
                    break;
                }
            }
            return possible;
        }

        // GET: ClassRoomAllocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            if (classRoomAllocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", classRoomAllocation.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", classRoomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", classRoomAllocation.RoomId);
            return View(classRoomAllocation);
        }

        // POST: ClassRoomAllocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllocationId,DepartmentId,CourseId,RoomId,Day,StartTime,EndTime")] ClassRoomAllocation classRoomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classRoomAllocation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", classRoomAllocation.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", classRoomAllocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", classRoomAllocation.RoomId);
            return View(classRoomAllocation);
        }

        // GET: ClassRoomAllocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            if (classRoomAllocation == null)
            {
                return HttpNotFound();
            }
            return View(classRoomAllocation);
        }

        // POST: ClassRoomAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassRoomAllocation classRoomAllocation = db.ClassRoomAllocations.Find(id);
            db.ClassRoomAllocations.Remove(classRoomAllocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UnAllocateAllClassrooms()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UnAllocateAllClassrooms(int allocationId = 0)
        {
            var allocatedClassrooms = db.ClassRoomAllocations.ToList();

            foreach (var classRoomAllocation in allocatedClassrooms)
            {
                classRoomAllocation.IsAllocated = false;
                db.Entry(classRoomAllocation).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
