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
    public class students_parentController : Controller
    {
        private MYDBprojectEntities db = new MYDBprojectEntities();

        // GET: students_parent
        /*public ActionResult Index()
        {
            var students_parent = db.students_parent.Include(s => s.student);
            return View(students_parent.ToList());
        }*/
        public ActionResult Index(string searchString)
        {
            var students_parent = from d in db.students_parent select d;
            if (!string.IsNullOrEmpty(searchString))
            {
                students_parent = students_parent.Where(d => d.students_parent_mothername.Contains(searchString) || d.students_parent_fathername.Contains(searchString) || d.students_parent_isstudent.Contains(searchString) || d.student.student_name.Contains(searchString));
            }
            return View(students_parent);
        }

        // GET: students_parent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students_parent students_parent = db.students_parent.Find(id);
            if (students_parent == null)
            {
                return HttpNotFound();
            }
            return View(students_parent);
        }

        // GET: students_parent/Create
        public ActionResult Create()
        {
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name");
            return View();
        }

        // POST: students_parent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "students_parent_id,students_parent_mobile,students_parent_email,students_parent_isstudent,students_parent_mothername,students_parent_fathername,student_id")] students_parent students_parent)
        {
            if (ModelState.IsValid)
            {
                db.students_parent.Add(students_parent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", students_parent.student_id);
            return View(students_parent);
        }

        // GET: students_parent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students_parent students_parent = db.students_parent.Find(id);
            if (students_parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", students_parent.student_id);
            return View(students_parent);
        }

        // POST: students_parent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "students_parent_id,students_parent_mobile,students_parent_email,students_parent_isstudent,students_parent_mothername,students_parent_fathername,student_id")] students_parent students_parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students_parent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", students_parent.student_id);
            return View(students_parent);
        }

        // GET: students_parent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students_parent students_parent = db.students_parent.Find(id);
            if (students_parent == null)
            {
                return HttpNotFound();
            }
            return View(students_parent);
        }

        // POST: students_parent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            students_parent students_parent = db.students_parent.Find(id);
            db.students_parent.Remove(students_parent);
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
