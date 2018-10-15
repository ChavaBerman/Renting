using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentingGown.Models;

namespace RentingGown.Controllers
{
    public class RentsController : Controller
    {
        private RentingGownDB db = new RentingGownDB();

        // GET: Rents
        public ActionResult Index()
        {
            var rents = db.Rents.Include(r => r.Gowns).Include(r => r.Gowns1).Include(r => r.Tenants).Include(r => r.Tenants1);
            return View(rents.ToList());
        }

        // GET: Rents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rents rents = db.Rents.Find(id);
            if (rents == null)
            {
                return HttpNotFound();
            }
            return View(rents);
        }

        // GET: Rents/Create
        public ActionResult Create()
        {
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture");
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture");
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname");
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname");
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_tenant,id_gown,payment,date,is_payed,is_returned")] Rents rents)
        {
            if (ModelState.IsValid)
            {
                db.Rents.Add(rents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture", rents.id_gown);
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture", rents.id_gown);
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname", rents.id_tenant);
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname", rents.id_tenant);
            return View(rents);
        }

        // GET: Rents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rents rents = db.Rents.Find(id);
            if (rents == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture", rents.id_gown);
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture", rents.id_gown);
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname", rents.id_tenant);
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname", rents.id_tenant);
            return View(rents);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_tenant,id_gown,payment,date,is_payed,is_returned")] Rents rents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture", rents.id_gown);
            ViewBag.id_gown = new SelectList(db.Gowns, "id_gown", "picture", rents.id_gown);
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname", rents.id_tenant);
            ViewBag.id_tenant = new SelectList(db.Tenants, "id_tenant", "fname", rents.id_tenant);
            return View(rents);
        }

        // GET: Rents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rents rents = db.Rents.Find(id);
            if (rents == null)
            {
                return HttpNotFound();
            }
            return View(rents);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rents rents = db.Rents.Find(id);
            db.Rents.Remove(rents);
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
