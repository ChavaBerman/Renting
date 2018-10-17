//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data;
//using System.Data.Entity;
//using System.Web;
//using System.Web.Mvc;
//using RentingGown.Models;
//using System.Net;
//using System.IO;
//using System.Web.Helpers;
//using System.Web.SessionState;

//namespace RentingGown.Controllers
//{
//    public class SearchDetails
//    {
//        public string stringSizes { get; set; }
//        public string is_long { get; set; }
//        public string is_light { get; set; }
//        public int? color { get; set; }
//        public int? id_catgory { get; set; }
//        public int? id_season { get; set; }
//        public int? price { get; set; }
//        public DateTime date { get; set; }

//    }
//    public class ConnectionController : Controller
//    {

//        private RentingGownDB db = new RentingGownDB();

//        public ActionResult AddGown(int? error)
//        {
//            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory");
//            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname");
//            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
//            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
//            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set");
//            ViewBag.color = new SelectList(db.Colors, "id_color", "color");
//            if (error == 1)
//            {
//                ViewBag.classs = "alert alert-danger";
//                ViewBag.ErrorExistsPassword = "לא קיימת סיסמה כזו.";
//            }
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AddGown(int password, int id_catgory, int id_season, string is_long, int price, string is_light, int color, int num_of_dress, string fileset, string arrFiles, string arrSizes)
//        {
//            System.Web.Helpers.WebImage photo = null;

//            var imagePath = "";


//            int error = 0;

//            var q = db.Renters.FirstOrDefault(p => p.password == password);
//            if (ModelState.IsValid && q != null)
//            {

//                string[] arrayFiles;
//                string[] arraySizes;
//                string[] arrfilenames;
//                arraySizes = arrSizes.Split(' ').ToArray();
//                arrayFiles = arrFiles.Split('?').ToArray();
//                int idGown = 0;
//                foreach (Gowns currentGown in db.Gowns)
//                {
//                    idGown = currentGown.id_gown;
//                }
//                if (arraySizes.Count() > 2)
//                {
//                    Sets set = new Sets();
//                    int id = 0;
//                    foreach (Sets currentset in db.Sets)
//                    {
//                        id = currentset.id_set;
//                    }
//                    set.num_of_set = arraySizes.Count() - 1;
//                    set.id_set = id + 1;
//                    arrfilenames = arrayFiles[0].Split('\\').ToArray();
//                    var filenameSet = Guid.NewGuid().ToString() + "_" + arrfilenames[arrfilenames.Count() - 1];
//                    set.picture = filenameSet;
//                    photo = WebImage.GetImageFromRequest("fileset");
//                    if (photo != null)
//                    {
//                        //Path.GetFileName(photo.FileName);
//                        imagePath = @"Images\" + filenameSet;

//                        photo.Save(@"~\" + imagePath);
//                    }
//                    int i = 0;

//                    foreach (string size in arraySizes)
//                    {

//                        if (size != "")
//                        {
//                            Gowns gown = new Gowns();
//                            arrfilenames = arrayFiles[i].Split('\\').ToArray();
//                            gown.id_gown = idGown + 1;
//                            gown.id_renter = q.id_renter;
//                            gown.id_catgory = id_catgory;
//                            var filename = Guid.NewGuid().ToString() + "_" + arrfilenames[arrfilenames.Count() - 1];
//                            gown.picture = filename.ToString();
//                            gown.id_season = id_season;
//                            if (is_long == "ארוך")
//                                gown.is_long = true;
//                            else gown.is_long = false;
//                            gown.price = price;
//                            if (is_light == "בהיר")
//                                gown.is_light = true;
//                            else gown.is_light = false;
//                            gown.color = color;
//                            gown.id_set = set.id_set;
//                            gown.size = int.Parse(size);
//                            photo = WebImage.GetImageFromRequest("file" + ++i);
//                            if (photo != null)
//                            {

//                                //photo.Resize((int)(photo.Width*0.8), (int)(photo.Height * 0.8));
//                                //Path.GetFileName(photo.FileName);
//                                imagePath = @"Images\" + filename;

//                                photo.Save(@"~\" + imagePath);
//                            }
//                            db.Sets.Add(set);
//                            db.Gowns.Add(gown);

//                        }

//                    }
//                }
//                else
//                {
//                    Gowns gown = new Gowns();
//                    gown.id_gown = idGown + 1;
//                    gown.id_renter = q.id_renter;
//                    gown.id_catgory = id_catgory;
//                    arrfilenames = arrayFiles[0].Split('\\').ToArray();
//                    var filename = Guid.NewGuid().ToString() + "_" + arrfilenames[arrfilenames.Count() - 1];
//                    gown.picture = filename.ToString();
//                    gown.id_season = id_season;
//                    if (is_long == "ארוך")
//                        gown.is_long = true;
//                    else gown.is_long = false;
//                    gown.price = price;
//                    if (is_light == "בהיר")
//                        gown.is_light = true;
//                    else gown.is_light = false;
//                    gown.color = color;
//                    gown.id_set = 1;
//                    gown.size = int.Parse(arraySizes[0]);

//                    photo = WebImage.GetImageFromRequest("file1");
//                    if (photo != null)
//                    {
//                        //Path.GetFileName(photo.FileName);
//                        imagePath = @"Images\" + filename;

//                        photo.Save(@"~\" + imagePath);
//                    }

//                    db.Gowns.Add(gown);
//                }
//                db.SaveChanges();
//                // return RedirectToAction("Index");
//                return RedirectToAction("Index", "Home");

//            }
//            else
//            {
//                if (q == null)
//                {
//                    error = 1;
//                }

//            }
//            ViewBag.id_catgory = new SelectList(db.Catgories, "id_catgory", "catgory");
//            ViewBag.id_renter = new SelectList(db.Renters, "id_renter", "fname");
//            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
//            ViewBag.id_season = new SelectList(db.Seasons, "id_season", "season");
//            ViewBag.id_set = new SelectList(db.Sets, "id_set", "id_set");
//            ViewBag.color = new SelectList(db.Colors, "id_color", "color");

//            return RedirectToAction("AddGown", new { error = error });


//        }
//        // GET: Connection
//        public ActionResult ShowGowns(List<Gowns> CurrentGowns)
//        {

//            return View(CurrentGowns);
//        }
//        public ActionResult AddRenter(int? error)
//        {
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area");
//            ViewBag.ErrorExists = "";
//            ViewBag.classs = "";
//            ViewBag.ErrorExistsPassword = "";
//            if (error == 2)
//            {
//                ViewBag.class2 = "alert alert-danger";
//                ViewBag.ErrorExistsPassword = "קיימת סיסמה כזו.";
//            }
//            if (error == 1)
//            {
//                ViewBag.class1 = "alert alert-danger";
//                ViewBag.ErrorExists = "קיים אדם כזה";
//            }
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AddRenter([Bind(Include = "id_renter,password,fname,lname,phone,cellphone,city,address,id_area")] Renters renters)
//        {
//            int error2 = 0;
//            var q1 = db.Renters.FirstOrDefault(p => p.password == renters.password);
//            var q = db.Renters.FirstOrDefault(p => p.fname == renters.fname && p.lname == renters.lname && p.phone == renters.phone && p.city == renters.city && p.address == renters.address && p.id_area == renters.id_area);
//            if (ModelState.IsValid && q == null && q1 == null)
//            {
//                db.Renters.Add(renters);
//                db.SaveChanges();
//                //  
//            }
//            else
//            {

//                if (q != null)
//                {
//                    error2 = 1;
//                }
//                if (q1 != null)
//                {

//                    error2 = 2;
//                }

//                return RedirectToAction("AddRenter", new { error = error2 });
//            }

//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", renters.id_area);
//            return RedirectToAction("AddGown", "Connection");
//        }
        

//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult Search(int? id_gown, int? id_catgory, int? id_season, int? color, int? min_price, int? max_price, string is_long, int? num_of_dress, int? num, string arr, DateTime? date)
//        //{

//        //    if (ModelState.IsValid)
//        //    {
//        //        List<Gowns> RealCurrentGowns = new List<Gowns>();
//        //        string[] arraysizes;
//        //        List<Gowns> CurrentGowns = db.Gowns.Where(p => p.color == color &&
//        //         (id_gown != null ? p.id_gown == id_gown : p.id_gown != 0) &&
//        //         id_catgory == p.id_catgory &&
//        //         id_season == p.id_season &&
//        //         (min_price != null ? p.price >= min_price : p.price != 0) &&
//        //         (max_price != null ? p.price <= max_price : p.price != 0) &&
//        //         (is_long == "ארוך" ? p.is_long == true : p.is_long == false)).ToList();
//        //        List<Gowns> myList = new List<Gowns>();
//        //        foreach (Gowns item in CurrentGowns)
//        //        {
//        //            if (db.Tenants_sets.Where(o => o.date == date && o.id_gown == item.id_gown).Count() == 0)
//        //                myList.Add(item);

//        //        }
//        //        CurrentGowns = myList;
//        //        List<Gowns> sets = new List<Gowns>();
//        //        if (num_of_dress > 1)
//        //        {
//        //            foreach (Gowns gown in CurrentGowns)
//        //            {

//        //                sets.Clear();
//        //                arraysizes = arr.Split(' ').ToArray();

//        //                foreach (string size in arraysizes)
//        //                {
//        //                    if (size != "")
//        //                    {
//        //                        var q = CurrentGowns.FirstOrDefault(u => u.id_set == gown.id_set && u.size == int.Parse(size));
//        //                        if (q != null)
//        //                            sets.Add(q);
//        //                    }
//        //                }

//        //                if (gown.Sets.num_of_set >= num_of_dress)
//        //                {



//        //                    if (sets.Count == num_of_dress)
//        //                    {
//        //                        foreach (Gowns item in sets)
//        //                        {
//        //                            var q2 = RealCurrentGowns.FirstOrDefault(p => p.id_gown == item.id_gown);
//        //                            if (q2 == null)
//        //                                RealCurrentGowns.Add(item);
//        //                        }
//        //                    }
//        //                }

//        //            }
//        //        }
//        //        else
//        //        {
//        //            arraysizes = arr.Split(' ').ToArray();
//        //            foreach (string size in arraysizes)
//        //            {

//        //                if (size != "")
//        //                {
//        //                    foreach (Gowns gown in CurrentGowns)
//        //                    {
//        //                        var q = CurrentGowns.FirstOrDefault(u => u.size == int.Parse(size) && u.id_gown == gown.id_gown);

//        //                        if (q != null)
//        //                        {
//        //                            var q2 = RealCurrentGowns.FirstOrDefault(p => p.id_gown == gown.id_gown);
//        //                            if (q2 == null)
//        //                                RealCurrentGowns.Add(gown);
//        //                        }

//        //                    }
//        //                }
//        //            }
//        //        }
//        //        if (Session["RequstedDate"] == null)
//        //            Session["RequstedDate"] = new DateTime();
//        //        Session["RequstedDate"] = date;
//        //        if (Session["RealCurrentGowns"] == null)
//        //        { Session["RealCurrentGowns"] = new List<Gowns>(); }
//        //        Session["RealCurrentGowns"] =RealCurrentGowns;
//        //        return View("ShowGowns", RealCurrentGowns);
//        //    }

//        //    return View();
//        //}
//        public ActionResult ShowSet(int? idSet)
//        {
//            if (idSet == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Sets set = db.Sets.FirstOrDefault(p => p.id_set == idSet);


//            return PartialView(set);
//        }

//        public ActionResult AddToTheBasket(int? id)
//        {
//            List<Gowns> gowns = new List<Gowns>();
//            Gowns gown = db.Gowns.FirstOrDefault(p => p.id_gown == id);

//            if (Session["listOfGowns"] == null)
//            {
//                Session["listOfGowns"] = new List<Gowns>();
//                (Session["listOfGowns"] as List<Gowns>).Add(gown);
//            }
//            else
//            {
//                var q = (Session["listOfGowns"] as List<Gowns>).FirstOrDefault(p => p.id_gown == gown.id_gown);
//                if (q == null)
//                    (Session["listOfGowns"] as List<Gowns>).Add(gown);
//            }
//            return RedirectToAction("ShowGowns", gowns);
//        }
//        public ActionResult ShowDetails()
//        {
//            List<Renters> renters = new List<Renters>();
//            foreach (Gowns gown in (Session["listOfGowns"] as List<Gowns>))
//            {
//                renters.Add(db.Renters.FirstOrDefault(p => p.id_renter == gown.id_renter));
//            }
//            if (Session["listRenters"] == null)
//                Session["listRenters"] = new List<Renters>();
//            (Session["listRenters"] as List<Renters>).AddRange(renters);
//            return View();
//        }
//        public ActionResult AddTenant()
//        {
//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area");
//            if (Session["listOfGowns"] == null)
//                return RedirectToAction("Index", "Home");
//            return View();
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AddTenant([Bind(Include = "id_tenant,fname,lname,phone,cellphone,address,id_area")] Tenants tenants)
//        {
//            var q = db.Tenants.FirstOrDefault(p => p.fname == tenants.fname && p.lname == tenants.lname && p.phone == tenants.phone && p.cellphone == tenants.cellphone && p.address == tenants.address && p.id_area == tenants.id_area);
//            int id = 0;
//            foreach (Tenants tenant in db.Tenants)
//            {
//                id = tenant.id_tenant;
//            }

//            if (q == null)
//            {

//                tenants.id_tenant = id + 1;
//                q = tenants;
//                db.Tenants.Add(tenants);
//            }

//            if (ModelState.IsValid)
//            {



//                //foreach (Gowns gown in (Session["listOfGowns"] as List<Gowns>))
//                //{
//                //    Tenants_sets NewTenant = new Tenants_sets();
//                //    NewTenant.id_gown = gown.id_gown;
//                //    NewTenant.id_tenant = q.id_tenant;
//                //    NewTenant.payment = gown.price;

//                //    NewTenant.date = (DateTime)Session["RequstedDate"];
//                //    NewTenant.is_payed = false;
//                //    NewTenant.is_returned = false;
//                //    db.Tenants_sets.Add(NewTenant);
//                //}
//                db.SaveChanges();
//                return RedirectToAction("ShowDetails", "Connection");
//            }


//            ViewBag.id_area = new SelectList(db.Areas, "id_area", "area", tenants.id_area);
//            return View(tenants);
//        }
//    }
//}