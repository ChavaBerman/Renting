//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using RentingGown.Models;

//namespace RentingGown.Controllers
//{
//    public class TenantsController : Controller
//    {
//        private RentingGownDB db = new RentingGownDB();

//        // GET: Tenants
//        public ActionResult Index()
//        {
//            var tenants = db.Tenants.Include(t => t.Areas);
//            return View(tenants.ToList());
//        }

//        // GET: Tenants/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Tenants tenants = db.Tenants.Find(id);
//            if (tenants == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tenants);
//        }

//        // GET: Tenants/Create
//        public ActionResult Create()
//        {
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area");
//            return View();
//        }

//        // POST: Tenants/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "id_tenant,fname,lname,phone,cellphone,address,id_area")] Tenants tenants)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Tenants.Add(tenants);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", tenants.id_area);
//            return View(tenants);
//        }

//        // GET: Tenants/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Tenants tenants = db.Tenants.Find(id);
//            if (tenants == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", tenants.id_area);
//            return View(tenants);
//        }

//        // POST: Tenants/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "id_tenant,fname,lname,phone,cellphone,address,id_area")] Tenants tenants)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(tenants).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", tenants.id_area);
//            return View(tenants);
//        }

//        // GET: Tenants/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Tenants tenants = db.Tenants.Find(id);
//            if (tenants == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tenants);
//        }

//        // POST: Tenants/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Tenants tenants = db.Tenants.Find(id);
//            db.Tenants.Remove(tenants);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
