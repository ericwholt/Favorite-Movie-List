using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Favorite_Movie_List.Models
{
    public class MovieAPIDAL
    {
        public static string APICall(string URL)
        {
            HttpWebRequest request = WebRequest.CreateHttp(URL);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            return APIText;
        }

        public static List<Movie> SearchMovie(string title)
        {
            string APIKey = "1c3b844b";

            string URL = $"http://www.omdbapi.com/?s={title}&apikey={APIKey}";

            string MovieText = APICall(URL);

            JToken movieJson = JToken.Parse(MovieText);

            List<JToken> moviesToken = movieJson["Search"].ToList();
            List<Movie> movies = new List<Movie>();

            foreach (JToken movie in moviesToken)
            {
                Movie m = new Movie(movie);
                movies.Add(m);
            }
            return movies;
        }


        
    }
}