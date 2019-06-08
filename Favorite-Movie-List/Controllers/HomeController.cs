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
        //Setup db context
        FavoriteMovieDBEntities db = new FavoriteMovieDBEntities();

        public ActionResult MovieResult(string Title)
        {
            //Get list of movies from API to pass to view
            List<Movie> movies = MovieAPIDAL.SearchMovie(Title);

            //Make sure movies list exists
            if (movies != null)
            {
                return View(movies);
            }
            else
            {
                //Show movie search 
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddFavorite(string movie)
        {
            //Create favorite database object based on model
            FavoriteMovy movy = new FavoriteMovy();
            movy.ImdbId = movie.Trim();
            movy.UserId = User.Identity.GetUserId();

            //Make sure user is logged in.
            if (movy.UserId == null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            //Find movie in favorites database
            List<FavoriteMovy> list = db.FavoriteMovies.Where(x => x.ImdbId == movy.ImdbId).ToList(); 

            //Check count if it is zero than the movie isn't in the favorites database.
            if (list.Count == 0)
            {
                db.FavoriteMovies.Add(movy);
                db.SaveChanges();
            }

            //Show favorites list
            return RedirectToAction("FavoriteList");
        }
        public ActionResult RemoveFavorite(int id)
        {
            //Find movie in the favorites database and store in Favorite Movy object
            FavoriteMovy movy = db.FavoriteMovies.Find(id);

            //Remove the movie from the favorites database
            db.FavoriteMovies.Remove(movy);
            db.SaveChanges();

            //Show favorites list
            return RedirectToAction("FavoriteList");

        }

        public ActionResult FavoriteList()
        {
            //Create list to store favorites db entires
            List<FavoriteMovy> favoriteEntry = new List<FavoriteMovy>();

            //Create list to store movie information from ombdID api 
            List<Movie> ListOfMovie = new List<Movie>();

            // populate List from database
            favoriteEntry = db.FavoriteMovies.ToList();

            //populate movie list from API bases on favorite entries in database
            foreach (FavoriteMovy movie in favoriteEntry)
            {
                ListOfMovie.Add(MovieAPIDAL.GetMovieById(movie.ImdbId));
            }

            //Create Favorite View Model
            FavoriteMovieVM favoriteMovieVM = new FavoriteMovieVM
            {
                ListOfMovie = ListOfMovie,
                FavoriteMovies = db.FavoriteMovies.ToList()
            };
            //favoriteMovieVM.ListOfMovie = ListOfMovie;
            //favoriteMovieVM.FavoriteMovies = db.FavoriteMovies.ToList();
            //var movies = new FavoriteMovieDBEntities();
            //movies.FavoriteMovies.ToList()

            //Show Favorite List
            return View(favoriteMovieVM);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}