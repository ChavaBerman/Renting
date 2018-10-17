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

    public class SearchDetails
    {
        public string stringSizes { get; set; }
        public string is_long { get; set; }
        public string is_light { get; set; }
        public int? color { get; set; }
        public int? id_catgory { get; set; }
        public int? id_season { get; set; }
        public int? price { get; set; }
        public DateTime date { get; set; }

    }
    public class GownsController : Controller
    {

        private RentingGownDB db = new RentingGownDB();
        public ActionResult Search()
        {
            //SelectList categories = new SelectList(db.Catgories, "id_catgory", "catgory");
            //SelectListItem selected = categories.Where(x => x.Value == "4").First();
            //selected.Selected = true;
            //SelectListItem g= categories.Where(p => p.Selected == true).First();
            //ViewBag.id_catgory = categories;
            if (Session["searchDetails"] != null)
            {
                SearchDetails details = (Session["searchDetails"] as SearchDetails);
                ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory", details.id_catgory);
                ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", details.id_season);
                ViewBag.color = new SelectList(db.Colors, "id_color", "color");
                ViewBag.stringSizes = details.stringSizes.Split(' ').ToArray();
                ViewBag.price = details.price;
                ViewBag.date = details.date.ToString("yyyy-MM-dd");
                //MainSearch(details.id_catgory, details.id_season, details.price, details.stringSizes, details.is_long, details.is_light, details.color);
            }
            else
            {
                ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory");
                ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname");
                ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
                ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
                ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set");
                ViewBag.color = new SelectList(db.Colors, "id_color", "color");
                ViewBag.price = 250;
                ViewBag.date = "2014-02-09";
                ViewBag.is_light = false;
                ViewBag.is_dark = false;
                ViewBag.is_long = false;
                ViewBag.is_short = false;
            }
            return View();
        }
        [HttpGet]
        public ActionResult MainSearch(double? lat, double? lng, DateTime date, int? id_catgory, int? id_season, int? price, string stringSizes, string is_long, string is_light, int? color)
        {
            Session["lat"] = lat;
            Session["lng"] = lng;
            SearchDetails searchDetails = new SearchDetails() { color = color, id_catgory = id_catgory, id_season = id_season, is_light = is_light, is_long = is_long, price = price, stringSizes = stringSizes, date = date };
            if (Session["searchDetails"] == null)
            {
                Session["searchDetails"] = new SearchDetails();
                Session["searchDetails"] = searchDetails;
            }
            else Session["searchDetails"] = searchDetails;

            List<Gowns> listAfterCheckLight = new List<Gowns>();
            List<Gowns> listAfterCheckLong = new List<Gowns>();
            List<Gowns> listAfterCheckColor = new List<Gowns>();
            List<Gowns> listAfterCheckDate = new List<Gowns>();
            List<Gowns> MainSearchResult = db.Gowns.Where(p => p.id_catgory == id_catgory && p.id_season == id_season && p.price < price + 50 && p.price > price - 50 && p.is_available == true).ToList();
            List<Gowns> myList = new List<Gowns>();
            //filter by sizes
            if (stringSizes != "")
            {
                string[] arraysizes = stringSizes.Split(' ').ToArray();
                foreach (string size in arraysizes)
                {
                    if (size != "")
                    {
                        foreach (Gowns gown in MainSearchResult)
                        {
                            if (gown.size == int.Parse(size))
                                myList.Add(gown);
                        }


                    }
                }
            }
            else myList = MainSearchResult;
            if (is_long != "")
            {
                foreach (Gowns gown in myList)
                {
                    if (gown.is_long == true && is_long == "ארוך" || gown.is_long == false && is_long == "קצר")
                        listAfterCheckLong.Add(gown);
                }
                myList = listAfterCheckLong;
            }
            if (is_light != "")
            {
                foreach (Gowns gown in myList)
                {
                    if (gown.is_light == true && is_light == "בהיר" || gown.is_light == false && is_light == "כהה")
                        listAfterCheckLight.Add(gown);
                }
                myList = listAfterCheckLight;
            }
            if (color != 1)
            {
                foreach (Gowns gown in myList)
                {
                    if (gown.color == color)
                        listAfterCheckColor.Add(gown);
                }
                myList = listAfterCheckColor;
            }

            foreach (Gowns gown in myList)
            {
                bool isAvailable = true;
                foreach (Rents rent in db.Rents)
                {
                    TimeSpan t1 = rent.date.Subtract(date);
                    TimeSpan t2 = date.Subtract(rent.date);
                    if (gown.id_gown == rent.id_gown && (t1.TotalDays < 10 && t1.TotalDays >= 0 || t2.TotalDays < 10 && t2.TotalDays >= 0))
                        isAvailable = false;

                }
                if (isAvailable == true)
                    listAfterCheckDate.Add(gown);
            }
            return PartialView(listAfterCheckDate);
        }

        public ActionResult showGown(int id)
        {
            Gowns gown = db.Gowns.FirstOrDefault(p => p.id_gown == id);
            ViewBag.season = db.Seasons.FirstOrDefault(p => p.id_season == gown.id_season).id_season;
            ViewBag.catgory = db.Catgories.FirstOrDefault(p => p.id_catgory == gown.id_catgory).catgory;
            return View(gown);
        }

        public ActionResult AddToTheBasket(int? id)
        {
            List<Gowns> gowns = new List<Gowns>();
            Gowns gown = db.Gowns.FirstOrDefault(p => p.id_gown == id);

            if (Session["listOfGowns"] == null)
            {
                Session["listOfGowns"] = new List<Gowns>();
                (Session["listOfGowns"] as List<Gowns>).Add(gown);
            }
            else
            {
                var q = (Session["listOfGowns"] as List<Gowns>).FirstOrDefault(p => p.id_gown == gown.id_gown);
                if (q == null)
                    (Session["listOfGowns"] as List<Gowns>).Add(gown);
            }
            return RedirectToAction("Search", "Gowns");
        }

        public ActionResult ShowBasket()
        {
            List<Gowns> gowns = new List<Gowns>();
            if (Session["listOfGowns"] != null)
                gowns = (Session["listOfGowns"] as List<Gowns>);
            return View(gowns);
        }

        public ActionResult RemoveFromBasket(int? id)
        {
            if (Session["listOfGowns"] != null)
            {
                List<Gowns> gowns = (Session["listOfGowns"] as List<Gowns>);
                Gowns gownForRemove = gowns.FirstOrDefault(p => p.id_gown == id);
                if (gownForRemove != null)
                    gowns.Remove(gownForRemove);
                Session["listOfGowns"] = gowns;
            }
            return RedirectToAction("ShowBasket");
        }
        // GET: Gowns
        public ActionResult Index()
        {
            var gowns = db.Gowns.Include(g => g.Catgories).Include(g => g.Renters).Include(g => g.Seasons).Include(g => g.Seasons1).Include(g => g.Sets).Include(g => g.Colors);
            return View(gowns.ToList());
        }

        // GET: Gowns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gowns gowns = db.Gowns.Find(id);
            if (gowns == null)
            {
                return HttpNotFound();
            }
            return View(gowns);
        }

        // GET: Gowns/Create
        public ActionResult Create()
        {
            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory");
            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname");
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set");
            ViewBag.color = new SelectList(db.Colors, "id_color", "color");
            return View();
        }

        // POST: Gowns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_gown,id_renter,id_catgory,picture,id_season,is_long,price,is_light,color,id_set,is_available,size")] Gowns gowns)
        {
            if (ModelState.IsValid)
            {
                db.Gowns.Add(gowns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory", gowns.id_catgory);
            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname", gowns.id_renter);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gowns.id_season);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gowns.id_season);
            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set", gowns.id_set);
            ViewBag.color = new SelectList(db.Colors, "id_color", "color", gowns.color);
            return View(gowns);
        }

        // GET: Gowns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gowns gowns = db.Gowns.Find(id);
            if (gowns == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory", gowns.id_catgory);
            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname", gowns.id_renter);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gowns.id_season);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gowns.id_season);
            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set", gowns.id_set);
            ViewBag.color = new SelectList(db.Colors, "id_color", "color", gowns.color);
            return View(gowns);
        }

        // POST: Gowns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_gown,id_renter,id_catgory,picture,id_season,is_long,price,is_light,color,id_set,is_available,size")] Gowns gowns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gowns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory", gowns.id_catgory);
            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname", gowns.id_renter);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gowns.id_season);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gowns.id_season);
            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set", gowns.id_set);
            ViewBag.color = new SelectList(db.Colors, "id_color", "color", gowns.color);
            return View(gowns);
        }

        // GET: Gowns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gowns gowns = db.Gowns.Find(id);
            if (gowns == null)
            {
                return HttpNotFound();
            }
            return View(gowns);
        }

        // POST: Gowns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gowns gowns = db.Gowns.Find(id);
            db.Gowns.Remove(gowns);
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
        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(string name, string phone, string address)
        {
            Tenants tenant = new Tenants { fname = name, phone = phone, address = address };
            db.Tenants.Add(tenant);
            db.SaveChanges();

            List<Renters> renters = new List<Renters>();
            foreach (Gowns gown in (Session["listOfGowns"] as List<Gowns>))
            {
                Tenants currentTenant = db.Tenants.FirstOrDefault(p => p.fname == name && p.phone == phone && p.address == address);
                Rents newRent = new Rents() { id_gown = gown.id_gown, id_tenant = currentTenant.id_tenant, payment = gown.price };
                if (Session["searchDetails"] != null)
                    newRent.date = ((SearchDetails)Session["searchDetails"]).date;
                db.Rents.Add(newRent);

                renters.Add(db.Renters.FirstOrDefault(p => p.id_renter == gown.id_renter));
            }
            db.SaveChanges();
            return View("RentsDetails", renters);
        }
    }
}
