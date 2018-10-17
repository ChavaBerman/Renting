using RentingGown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentingGown.Controllers
{
    public class DetailsLogin
    {
        string name { get; set; }
        string password { get; set; }
    }
    public class UserController : Controller
    {
        private RentingGownDB db = new RentingGownDB();
        // GET: User
        [HttpGet]
        public ActionResult Login(int? error)
        {
            if (error != null)
                ViewBag.error = "error";
            else ViewBag.error = "notError";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password)
        {

            Users user = db.Users.FirstOrDefault(u => u.username == name && u.password == password);
            if (user != null)
            {
                if (user.status == 1)
                {//מנהל
                    Session["user"] = new Users();
                    Session["user"] = user;
                    return RedirectToAction("Manager", "Manager");
                }

                else
                {  //משכיר
                    Renters renter = db.Renters.FirstOrDefault(p => p.id_renter == user.userid);
                    Session["user"] = new Renters();
                    Session["user"] = renter;
                    return RedirectToAction("Renter", "Renter");
                }
            }
            return RedirectToAction("Login", new { error = 1 });
        }
[HttpGet]
public ActionResult Register(int? error)
{
    if (error == 1)
    {
        ViewBag.errorClass = "error";
    }
    else
    {
        ViewBag.errorClass = "";
    }
    return View();
}
[HttpPost]
public ActionResult Register(string fname, string lname, string phone, string cellphone, string address, string username, string password)
{
    Users user = db.Users.FirstOrDefault(p => p.password == password && p.username == username);
    if (user != null)
        return RedirectToAction("Register", new { error = 1 });
    Renters renter = new Renters() { fname = fname, lname = lname, phone = phone, cellphone = cellphone, address = address,is_active=true };
    db.Renters.Add(renter);
    db.SaveChanges();
    renter = db.Renters.FirstOrDefault(p => p.fname == fname && p.lname == lname && p.phone == phone && p.cellphone == cellphone && p.address == address);
    int userid = db.Renters.Count();
    if (renter != null)
        userid = renter.id_renter;
    user = new Users() { status = 2, password = password, username = username, userid = userid };
    db.Users.Add(user);
    db.SaveChanges();
    Session["user"] = new Renters();
    Session["user"] = renter;  
    return RedirectToAction("Renter", "Renter");
}
[HttpGet]
public ActionResult LogOut()
{
    Session["user"] = null;
    return RedirectToAction("Index", "Home");
}
    }
}