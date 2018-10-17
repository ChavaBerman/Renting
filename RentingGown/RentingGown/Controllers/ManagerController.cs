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
    public class ManagerController : Controller
    {
        private RentingGownDB db = new RentingGownDB();
        // GET: Manager
        public ActionResult Manager()
        {
            return View("Manager");
        }
        public ActionResult ShowRenters()
        {
            List<Renters> renters = new List<Renters>();
            renters=db.Renters.Where(p=>p.is_active!=false).ToList() ;
            return View(renters);
        }
        public ActionResult ShowGowns()
        {
            List<Gowns> gowns = new List<Gowns>();
              gowns=  db.Gowns.Where(p=>p.is_available==true).ToList();
            return View(gowns);
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
        public ActionResult EditGown(int id_gown, int id_catgory, int id_season, string is_long, int price, string is_light, int color, string picture, int size)
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

            return RedirectToAction("ShowGowns");
        }
        public ActionResult EditRenter(int? id)
        {
            Renters renter = db.Renters.FirstOrDefault(p => p.id_renter == id);
            return View(renter);
        }
        [HttpPost]
        public ActionResult EditRenter([Bind(Include = "id_renter,fname,lname,phone,cellphone,address")] Renters oldRenter)
        {
          Renters renter = db.Renters.FirstOrDefault(p => p.id_renter == oldRenter.id_renter);
            renter.fname = oldRenter.fname;
            renter.lname = oldRenter.lname;
            renter.phone = oldRenter.phone;
            renter.cellphone = oldRenter.cellphone;
            renter.address = oldRenter.address;
            db.SaveChanges();
            return RedirectToAction("ShowRenters");
        }
        [HttpGet]
        public ActionResult ShowRents()
        {
            List<Rents> rents = db.Rents.ToList();
            return View(rents);
        }

        public ActionResult DeleteGown(int? id)
        {
            Gowns gown = db.Gowns.FirstOrDefault(p => p.id_gown == id);
            if (gown != null)
                gown.is_available = false;
            db.SaveChanges();
            return RedirectToAction("ShowGowns");
        }
        public ActionResult DeleteRenter(int? id)
        {
            Renters renter = db.Renters.FirstOrDefault(p => p.id_renter == id);
            if (renter != null)
                renter.is_active = false;
            db.SaveChanges();
            return RedirectToAction("ShowRenters");
        }
    }
}