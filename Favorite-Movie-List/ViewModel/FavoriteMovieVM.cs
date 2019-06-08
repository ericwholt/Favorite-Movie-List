using Favorite_Movie_List.Models;
using System.Collections.Generic;

namespace Favorite_Movie_List.ViewModel
{
    public class FavoriteMovieVM
    {
        public List<FavoriteMovy> FavoriteMovies { get; set; }
        public List<Movie> ListOfMovie { get; set; }
    }
}
