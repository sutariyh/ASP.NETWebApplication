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
    public class paymentsController : Controller
    {
        private MYDBprojectEntities db = new MYDBprojectEntities();

        // GET: payments
        /*public ActionResult Index()
        {
            var payments = db.payments.Include(p => p.inventory).Include(p => p.student);
            return View(payments.ToList());
        }*/
        public ActionResult Index(string searchString)
        {
            var payments = from c in db.payments select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                payments = payments.Where(c => c.payment_category.Contains(searchString) || c.student.student_name.Contains(searchString));
            }
            return View(payments);
        }

        // GET: payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payments.FirstOrDefault(i => i.payment_id == id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: payments/Create
        public ActionResult Create()
        {
            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "inventory_category");
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name");
            return View();
        }

        // POST: payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "payment_id,student_id,inventory_id,payment_category,payment_amount,payment_date")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "inventory_category", payment.inventory_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", payment.student_id);
            return View(payment);
        }

        // GET: payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payments.FirstOrDefault(i => i.payment_id == id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "inventory_category", payment.inventory_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", payment.student_id);
            return View(payment);
        }

        // POST: payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "payment_id,student_id,inventory_id,payment_category,payment_amount,payment_date")] payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.inventory_id = new SelectList(db.inventories, "inventory_id", "inventory_category", payment.inventory_id);
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", payment.student_id);
            return View(payment);
        }

        // GET: payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payments.FirstOrDefault(i => i.payment_id == id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payment payment = db.payments.FirstOrDefault(i => i.payment_id == id);
            db.payments.Remove(payment);
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
