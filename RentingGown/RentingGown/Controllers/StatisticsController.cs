using RentingGown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentingGown.Controllers
{
    public class StatisticsController : Controller
    {
        private static RentingGownDB db = new RentingGownDB();
        // GET: Statistics
        public ActionResult Statistics()
        {
            ViewBag.colors = PopularColors();

            return View();
        }

        public static Dictionary<string, string> PopularColors()
        {
            Dictionary<string, string> colors = new Dictionary<string, string>();
            foreach (Colors color in db.Colors)
            {
                int sum = 0;
                foreach (Rents rent in db.Rents)
                {
                    if (rent.Gowns.color == color.id_color)
                        sum++;

                }
                if (sum > 0)
                    colors.Add(color.color, sum.ToString());
            }
            return colors;
        }
    }
}