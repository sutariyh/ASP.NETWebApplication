﻿using System;
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
    public class ranksController_old : Controller
    {
        private MYDBprojectEntities db = new MYDBprojectEntities();

        // GET: ranks
        /*public ActionResult Index()
        {
            var ranks = db.ranks.Include(r => r.student);
            return View(ranks.ToList());
        }*/
        public ActionResult Index(string searchString)
        {
            var ranks = from c in db.ranks select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                ranks = ranks.Where(c => c.rank_beltcolor.Contains(searchString));
            }
            return View(ranks);
        }

        // GET: ranks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rank rank = db.ranks.Find(id);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // GET: ranks/Create
        public ActionResult Create()
        {
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name");
            return View();
        }

        // POST: ranks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rank_id,student_id,rank_beltcolor,rank_date")] rank rank)
        {
            //rank.rank_beltcolor = ViewBag.rank_beltcolor;
            if (ModelState.IsValid)
            {
                db.ranks.Add(rank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", rank.student_id);
            return View(rank);
        }

        // GET: ranks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rank rank = db.ranks.Find(id);
            if (rank == null)
            {
                return HttpNotFound();
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", rank.student_id);
            return View(rank);
        }

        // POST: ranks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rank_id,student_id,rank_beltcolor,rank_date")] rank rank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_name", rank.student_id);
            return View(rank);
        }

        // GET: ranks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rank rank = db.ranks.Find(id);
            if (rank == null)
            {
                return HttpNotFound();
            }
            return View(rank);
        }

        // POST: ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rank rank = db.ranks.Find(id);
            db.ranks.Remove(rank);
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
