using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBapp.Models;

namespace DBapp.Controllers
{
    public class attendancesController : Controller
    {
        private MYDBprojectEntities db = new MYDBprojectEntities();

        // GET: attendances
        /*public ActionResult Index()
        {
            var attendances = db.attendances.Include(a => a.classs).Include(a => a.student);
            return View(attendances.ToList());
        }*/
        public ActionResult Index(string searchString)
        {
            var attendances = from c in db.attendances select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                attendances = attendances.Where(c => c.attendance_attended.Contains(searchString) || c.student.student_name.Contains(searchString) || c.attendance_date.ToString().Contains(searchString));
            }
            return View(attendances);
        }

        // GET: attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.FirstOrDefault(i => i.attendance_id == id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: attendances/Create
        public ActionResult Create()
        {
            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level");
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name");
            return View();
        }

        // POST: attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "attendance_id,student_id,class_id,attendance_attended,attendance_date")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level", attendance.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", attendance.student_id);
            return View(attendance);
        }

        // GET: attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.FirstOrDefault(i => i.attendance_id == id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level", attendance.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", attendance.student_id);
            return View(attendance);
        }

        // POST: attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "attendance_id,student_id,class_id,attendance_attended,attendance_date")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level", attendance.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", attendance.student_id);
            return View(attendance);
        }

        // GET: attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.FirstOrDefault(i => i.attendance_id == id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            attendance attendance = db.attendances.FirstOrDefault(i => i.attendance_id == id);
            db.attendances.Remove(attendance);
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
    }
}
