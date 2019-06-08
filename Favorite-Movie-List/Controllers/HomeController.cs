using Favorite_Movie_List.Models;
using Favorite_Movie_List.ViewModel;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Favorite_Movie_List.Controllers
{
    public class HomeController : Controller
    {
        FavoriteMovieDBEntities db = new FavoriteMovieDBEntities();

        public ActionResult MovieResult(string Title)
        {
            List<Movie> movies = MovieAPIDAL.SearchMovie(Title);
            return View(movies);
        }

        public ActionResult AddFavorite(string movie)
        {
            FavoriteMovy movy = new FavoriteMovy();
            movy.ImdbId = movie;

            movy.UserId = User.Identity.GetUserId();
            if (movy.UserId == null)
            {

                return RedirectToAction(nameof(AccountController.Login), "Account");


            }

            db.FavoriteMovies.Add(movy);
            db.SaveChanges();
            return RedirectToAction("FavoriteList");
        }
        public ActionResult RemoveFavorite(int id)
        {
            FavoriteMovy movy = db.FavoriteMovies.Find(id);
            //movy.UserId = User.Identity.GetUserId();

            db.FavoriteMovies.Remove(movy);
            db.SaveChanges();
            return RedirectToAction("FavoriteList");

        }

        public ActionResult FavoriteList()
        {

            //FavoriteMovieVM favoriteMovieVM = new FavoriteMovieVM();
            List<FavoriteMovy> favoriteEntry = new List<FavoriteMovy>();
            List<Movie> ListOfMovie = new List<Movie>();
            favoriteEntry = db.FavoriteMovies.ToList();

            foreach (FavoriteMovy movie in favoriteEntry)
            {
                ListOfMovie.Add(MovieAPIDAL.GetMovieById(movie.ImdbId));
            }

            var favoriteMovieVM = new FavoriteMovieVM
            {
                ListOfMovie = ListOfMovie,
                FavoriteMovies = db.FavoriteMovies.ToList()
            };
            favoriteMovieVM.ListOfMovie = ListOfMovie;
            favoriteMovieVM.FavoriteMovies = db.FavoriteMovies.ToList();
            //var movies = new FavoriteMovieDBEntities();
            //movies.FavoriteMovies.ToList()
            return View(favoriteMovieVM);
        }
        public ActionResult Details( string ImbdId)
        {                                           
            Movie IMBD = MovieAPIDAL.GetMovieById(ImbdId);
            return View(IMBD); 

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