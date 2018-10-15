using RentingGown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RentingGown.Controllers
{
    public class RenterController : Controller
    {
        private RentingGownDB db = new RentingGownDB();
        // GET: Renter
        public ActionResult Renter(int? msg)
        {
            ViewBag.msg = msg;
            return View();
        }
        // GET: Gowns/Create
        public ActionResult AddGown(int? error)
        {
            //--------------------------
            //if user is not loged in error=1
            //-----------------------------
            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory");
            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname");
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set");
            ViewBag.color = new SelectList(db.Colors, "id_color", "color");
            return View();
        }
        [HttpPost]
        public ActionResult AddGown(int id_catgory, int id_season, string is_long, int price, string is_light, string color, string picture, int size)
        {
            if (Session["user"] != null)
            {
                Gowns gown = new Gowns() { id_catgory = id_catgory, id_season = id_season, is_light = (is_light == "בהיר"), is_long = (is_long == "ארוך"), price = price, size = size,is_available=true };
                gown.id_renter = (Session["user"] as Renters).id_renter;
                Colors newColor = new Colors() { color = color };
                db.Colors.Add(newColor);
                db.SaveChanges();
                int colorId = db.Colors.First(p => p.color == color).id_color;
                gown.color = colorId;
                WebImage photo = WebImage.GetImageFromRequest("picture");
                var PictureName = Guid.NewGuid().ToString() + ".jpeg";
                gown.picture = PictureName;
                if (photo != null)
                {
                    var imagePath = @"Images\" + PictureName;
                    photo.Save(@"~\" + imagePath);
                }
                db.Gowns.Add(gown);
                db.SaveChanges();
            }
            return RedirectToAction("Renter", "Renter", new { msg = 1 });
        }
        public ActionResult DeleteGown(int? id)
        {
            string msg = "";
            if (id != null)
            {
                List<Rents> gownUses = db.Rents.Where(p => p.id_gown == id && p.date > DateTime.Now).ToList();
                 msg = "";
                foreach (Rents item in gownUses)
                {
                    msg += item.date.ToString();
                }
                ViewBag.msg = msg;
                db.Gowns.First(p => p.id_gown == id).is_available = false;
                db.SaveChanges();
            }
            else ViewBag.msg = "";
            int idRener = (Session["user"] as Renters).id_renter;
            List<Gowns> currentRenterGowns = db.Gowns.Where(p => p.id_renter == idRener).ToList();
            return RedirectToAction("ShowMyGowns",new { msg=msg});
        }
        public ActionResult DeleteGownPost(int? id)
        {
            Gowns gown = db.Gowns.First(p => p.id_gown == id);
            List<Rents> gownUses = db.Rents.Where(p => p.id_gown == id && p.date > DateTime.Now).ToList();
            if (gownUses.Count() > 0)
                return RedirectToAction("DeleteGown", new { id });
            gown.is_available = false;
            db.SaveChanges();
            return RedirectToAction("DeletGown");
        }
        public ActionResult ShowMyGowns(string msg)
        {
            if (msg != null)
                ViewBag.msg = msg;
            else ViewBag.msg = "noMsg";
            List<Gowns> myGowns = new List<Gowns>();
            if (Session["user"] != null)
            {
                int id = (Session["user"] as Renters).id_renter;
                myGowns = db.Gowns.Where(p =>p.id_renter==id&&p.is_available==true).ToList();
            }
            return View(myGowns);
        }
        public ActionResult EditGown(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gowns gown = db.Gowns.Find(id);
            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory", gown.id_catgory);
            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season", gown.id_season);
            ViewBag.color = new SelectList(db.Colors, "id_color", "color", gown.color);
            return View(gown);
        }
        [HttpPost]
        public ActionResult EditGown(int id_gown,int id_catgory, int id_season, string is_long, int price, string is_light, int color, string picture, int size)
        {
            Gowns gown = db.Gowns.Find(id_gown);
            gown.id_catgory = id_catgory;
            gown.id_season = id_season;
            if (is_long == "ארוך")
                gown.is_long = true;
            else gown.is_long = false;
            if (is_light == "בהיר")
                gown.is_light = true;
            else gown.is_light = false;
            gown.price = price;
            gown.color = color;
            gown.size = size;
            if (picture != null)
            {
                WebImage photo = WebImage.GetImageFromRequest("picture");
                var PictureName = Guid.NewGuid().ToString() + ".jpeg";
                gown.picture = PictureName;
                if (photo != null)
                {
                    var imagePath = @"Images\" + PictureName;
                    photo.Save(@"~\" + imagePath);
                }
            }
            db.SaveChanges();

            return RedirectToAction("Renter");
        }
        public ActionResult EditProfile()
        {
            Renters renter = new Renters();
            if (Session["user"] != null)
            {
               renter = (Session["user"] as Renters);
            }
            return View(renter);
        }
        [HttpPost]
        public ActionResult EditProfile([Bind(Include = "id_renter,fname,lname,phone,cellphone,address")] Renters oldRenter)
        {
            Renters renter = db.Renters.FirstOrDefault(p => p.id_renter == oldRenter.id_renter);
            renter.fname = oldRenter.fname;
            renter.lname = oldRenter.lname;
            renter.phone = oldRenter.phone;
            renter.cellphone = oldRenter.cellphone;
            renter.address = oldRenter.address;
            db.SaveChanges();
            if (Session["user"] != null)
                Session["user"] = renter;
            return RedirectToAction("Renter");
        }

    }
}