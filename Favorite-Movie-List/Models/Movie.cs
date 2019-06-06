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
        }

    }
}