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

        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult FavoriteList()
        {
            //Create list to store favorites db entires
            List<FavoriteMovy> favorites = new List<FavoriteMovy>();

            //Create list to store movie information from ombdID api 
            List<Movie> ListOfMovie = new List<Movie>();

            string userId = User.Identity.GetUserId();
            // populate List from database
            favorites = db.FavoriteMovies.Where(x => x.UserId == userId).ToList();
            //favorites = db.FavoriteMovies.ToList();

            //populate movie list from API bases on favorite entries in database
            foreach (FavoriteMovy favorite in favorites)
            {
                Movie movie = MovieAPIDAL.GetMovieById(favorite.ImdbId);
                if (movie != null)
                {
                    ListOfMovie.Add(movie);
                }
            }

            //Create Favorite View Model
            FavoriteMovieVM favoriteMovieVM = new FavoriteMovieVM
            {
                ListOfMovie = ListOfMovie,
                FavoriteMovies = db.FavoriteMovies.ToList()
            };

            //Show Favorite List
            return View(favoriteMovieVM);
        }

        public ActionResult Details(string movieId)
        {
            if (movieId != null)
            {
                //Get movie details from API
                Movie IMBD = MovieAPIDAL.GetMovieById(movieId.Trim());

                //Show Details view
                return View(IMBD);
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddFavorite(string movie)
        {
            if (movie != null)
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
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult RemoveFavorite(string id)
        {
            //Make sure parameter was supplied. If parameter is an int it will error out if not parameter is provided in url.
            if (id != null)
            {
                //If we can parse the string to an id
                if (int.TryParse(id, out int movieId))
                {
                    //Find movie in the favorites database and store in Favorite Movy object
                    FavoriteMovy movy = db.FavoriteMovies.Find(movieId);

                    //Check if we found a movie in the favorites database before trying to remove
                    if (movy != null)
                    {
                        //Remove the movie from the favorites database
                        db.FavoriteMovies.Remove(movy);
                        db.SaveChanges();
                    }

                    //Show favorites list
                    return RedirectToAction("FavoriteList");
                }
                else
                {
                    //No id send back to index
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //id is null send back to index
                return RedirectToAction("Index");
            }
        }

        //    public ActionResult RemoveFavorite(string movieImdb, string loggedInUserId)
        //    {
        //        //Make sure parameter was supplied. If parameter is an int it will error out if not parameter is provided in url.
        //        if (movieImdb != null && loggedInUserId != null)
        //        {
        //            //Find movie in the favorites database and store in Favorite Movy object
        //            List<FavoriteMovy> movyies = db.FavoriteMovies.ToList();
        //            for (int i = 0; i < movyies.Count; i++)
        //            {
        //                if (movyies[i].ImdbId.Trim() == movieImdb.Trim() && movyies[i].UserId == loggedInUserId)
        //                {
        //                    if (movyies[i] != null)
        //                    {
        //                        //Remove the movie from the favorites database
        //                        db.FavoriteMovies.Remove(movyies[i]);
        //                        db.SaveChanges();
        //                    }
        //                }
        //            }
        //            //Check if we found a movie in the favorites database before trying to remove


        //            //Show favorites list
        //            return RedirectToAction("FavoriteList");

        //        }
        //        else
        //        {
        //            //id is null send back to index
        //            return RedirectToAction("Index");
        //        }
        //    }

    }
}