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
    public class student_regController : Controller
    {
        private MYDBprojectEntities db = new MYDBprojectEntities();

        // GET: student_reg
        /*public ActionResult Index()
        {
            var student_reg = db.student_reg.Include(s => s.classs).Include(s => s.student);
            return View(student_reg.ToList());
        }*/
        public ActionResult Index(string searchString)
        {
            var student_reg = from c in db.student_reg select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                student_reg = student_reg.Where(c => c.student_reg_registered.Contains(searchString));
            }
            return View(student_reg);
        }

        // GET: student_reg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_reg student_reg = db.student_reg.FirstOrDefault(i => i.student_reg_id == id);
            if (student_reg == null)
            {
                return HttpNotFound();
            }
            return View(student_reg);
        }

        // GET: student_reg/Create
        public ActionResult Create()
        {
            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level");
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name");
            return View();
        }

        // POST: student_reg/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_reg_id,student_id,class_id,student_reg_registered")] student_reg student_reg)
        {
            if (ModelState.IsValid)
            {
                db.student_reg.Add(student_reg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level", student_reg.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", student_reg.student_id);
            return View(student_reg);
        }

        // GET: student_reg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_reg student_reg = db.student_reg.FirstOrDefault(i => i.student_reg_id == id);
            if (student_reg == null)
            {
                return HttpNotFound();
            }
            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level", student_reg.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", student_reg.student_id);
            return View(student_reg);
        }

        // POST: student_reg/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_reg_id,student_id,class_id,student_reg_registered")] student_reg student_reg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_reg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.class_id = new SelectList(db.classses, "class_id", "class_level", student_reg.class_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", student_reg.student_id);
            return View(student_reg);
        }

        // GET: student_reg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_reg student_reg = db.student_reg.FirstOrDefault(i => i.student_reg_id == id);
            if (student_reg == null)
            {
                return HttpNotFound();
            }
            return View(student_reg);
        }

        // POST: student_reg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student_reg student_reg = db.student_reg.FirstOrDefault(i => i.student_reg_id == id);
            db.student_reg.Remove(student_reg);
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
