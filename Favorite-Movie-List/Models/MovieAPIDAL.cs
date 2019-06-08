using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

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
            List<Movie> movies = new List<Movie>();
            string APIKey = ConfigReaderDAL.ReadSetting("APIKey");

            string URL = $"http://www.omdbapi.com/?s={title}&apikey={APIKey}";

            string MovieText = APICall(URL);

            JToken movieJson = JToken.Parse(MovieText);
            if (movieJson["Response"].ToString() != "False")
            {
                List<JToken> moviesToken = movieJson["Search"].ToList();

                foreach (JToken movie in moviesToken)
                {
                    Movie m = new Movie(movie);
                    movies.Add(m);
                }
            }

            return movies;
        }

        public static Movie GetMovieById(string id)
        {
            id = id.Trim();
            string APIKey = ConfigReaderDAL.ReadSetting("APIKey");

            string URL = $"http://www.omdbapi.com/?i={id}&apikey={APIKey}";

            string MovieText = APICall(URL);

            JToken movieJson = JToken.Parse(MovieText);

            Movie movie = new Movie(movieJson);

            return movie;
        }


    }
}