using Favorite_Movie_List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Favorite_Movie_List.Controllers
{
    public class HomeController : Controller
    {
        FavoriteMovieDBEntities db = new FavoriteMovieDBEntities();

        public ActionResult MovieResult( string Title )
        {
            List<Movie> movies =MovieAPIDAL.SearchMovie(Title);
            return View(movies);
        }

        public ActionResult AddFavorite(string movie)
        {
            FavoriteMovy movy = new FavoriteMovy();
            movy.ImdbId = movie;
            
            movy.UserId = User.Identity.GetUserId();
            if(movy.UserId == null)
            {
                return RedirectToAction("FavoriteList");
            }

            db.FavoriteMovies.Add(movy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFavorite( string movie)
        {
            FavoriteMovy movy = new FavoriteMovy();
            movy.ImdbId = movie;
            //movy.UserId = User.Identity.GetUserId();
          
            db.FavoriteMovies.Remove(movy);
            db.SaveChanges();
            return RedirectToAction("FavoriteList");

        }

        public ActionResult FavoriteList()
        {
            var movies = new FavoriteMovieDBEntities();

            return View(movies.FavoriteMovies.ToList());
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