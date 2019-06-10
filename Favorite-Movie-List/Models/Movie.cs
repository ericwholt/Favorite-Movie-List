using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favorite_Movie_List.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public string Actors { get; set; }  
        public string Rated { get; set; }
        public string Genre  { get; set; }


        public Movie()
        {

        }

        public Movie(JToken json)
        {
            Title = json["Title"].ToString();
            Year = json["Year"].ToString();
            ImdbId = json["imdbID"].ToString();
            Type = json["Type"].ToString();
            Poster = json["Poster"].ToString();
            if (json["Plot"] != null)
            {
                Plot = json["Plot"].ToString();
            }
            if (json["Actors"] !=null)
            {
                Actors = json["Actors"].ToString();
            }
           
            if(json["Rated"] != null)
            {
                Rated = json["Rated"].ToString();
            }
            if (json["Genre"] != null)
            {
                Genre = json["Genre"].ToString();
            }
                        
        }

    }
}