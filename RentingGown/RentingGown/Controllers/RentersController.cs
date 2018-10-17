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
//    public class RentersController : Controller
//    {
//        private RentingGownDB db = new RentingGownDB();

//        // GET: Renters
//        public ActionResult Index()
//        {
//            var renters = db.Renters.Include(r => r.Areas);
//            return View(renters.ToList());
//        }

//        // GET: Renters/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Renters renters = db.Renters.Find(id);
//            if (renters == null)
//            {
//                return HttpNotFound();
//            }
//            return View(renters);
//        }

//        // GET: Renters/Create
//        public ActionResult Create()
//        {
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area");
//            ViewBag.ErrorExists = "";
//            ViewBag.classs = "";
//            ViewBag.ErrorExistsPassword = "";

//            return View();
//        }

//        // POST: Renters/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "id_renter,password,fname,lname,phone,cellphone,city,address,id_area")] Renters renters)
//        {
//            var q1 = db.Renters.Where(p => p.password == renters.password).FirstOrDefault();
//            var q = db.Renters.Where(p => p.fname == renters.fname && p.lname == renters.lname && p.phone == renters.phone && p.city == renters.city && p.address == renters.address && p.id_area == renters.id_area).FirstOrDefault();
//            if (ModelState.IsValid && q == null && q1 == null)
//            {
//                db.Renters.Add(renters);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                ViewBag.classs = "alert alert-danger";
//                if (q != null)
//                {
//                    ViewBag.ErrorExists = "קיים אדם כזה כבר, נסה שנית.";
//                }
//                if(q1!=null)
//                {
//                    ViewBag.ErrorExists = "קיימת סיסמה כזו.";
//                }
//            }

//                ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", renters.id_area);
//                return View(renters);
//            }

//            // GET: Renters/Edit/5
//            public ActionResult Edit(int? id)
//            {
//                if (id == null)
//                {
//                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//                }
//                Renters renters = db.Renters.Find(id);
//                if (renters == null)
//                {
//                    return HttpNotFound();
//                }
//                ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", renters.id_area);
//                return View(renters);
//            }

//        // POST: Renters/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "id_renter,password,fname,lname,phone,cellphone,city,address,id_area")] Renters renters)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(renters).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", renters.id_area);
//            return View(renters);
//        }

//        // GET: Renters/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Renters renters = db.Renters.Find(id);
//            if (renters == null)
//            {
//                return HttpNotFound();
//            }
//            return View(renters);
//        }

//        // POST: Renters/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Renters renters = db.Renters.Find(id);
//            db.Renters.Remove(renters);
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
