using Favorite_Movie_List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Favorite_Movie_List.Controllers
{
    public class HomeController : Controller
    {
        FavoriteMovieDBEntities db = new FavoriteMovieDBEntities();

        public ActionResult MovieResult()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}