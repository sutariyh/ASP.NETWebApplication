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
    public class classsesController : Controller
    {
        private MYDBprojectEntities db = new MYDBprojectEntities();

        // GET: classses
        /*public ActionResult Index()
        {
            var classses = db.classses.Include(c => c.student);
            return View(classses.ToList());
        }*/
        public ActionResult Index(string searchString)
        {
            var classses = from c in db.classses select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                classses = classses.Where(c => c.class_day.Contains(searchString) || c.class_level.Contains(searchString) || c.student.student_name.Contains(searchString));
            }
            return View(classses);
        }

        // GET: classses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            classs classs = db.classses.Find(id);
            if (classs == null)
            {
                return HttpNotFound();
            }
            return View(classs);
        }

        // GET: classses/Create
        public ActionResult Create()
        {
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name");
            return View();
        }

        // POST: classses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "class_id,class_level,class_day,class_starttime,class_endtime,student_id")] classs classs)
        {
            if (ModelState.IsValid)
            {
                db.classses.Add(classs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", classs.student_id);
            return View(classs);
        }

        // GET: classses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            classs classs = db.classses.Find(id);
            if (classs == null)
            {
                return HttpNotFound();
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", classs.student_id);
            return View(classs);
        }

        // POST: classses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "class_id,class_level,class_day,class_starttime,class_endtime,student_id")] classs classs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", classs.student_id);
            return View(classs);
        }

        // GET: classses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            classs classs = db.classses.Find(id);
            if (classs == null)
            {
                return HttpNotFound();
            }
            return View(classs);
        }

        // POST: classses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            classs classs = db.classses.Find(id);
            db.classses.Remove(classs);
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
