using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class Movie
    {
        public Movie()
        {
        }

        public Movie(int v1, string v2, int v3, string v4, string v5, decimal v6)
        {
            ID = v1;
            Title = v2;
            ReleaseYear = v3;
            Genre = v4;
            Director = v5;
            Rating = v6;
        }

        public int ID { get; set; }
        public String Title { get; set; }
        public int ReleaseYear { get; set; }
        public String Genre { get; set; }
        
        public String Director  { get; set; }
        public decimal Rating { get; set; }

        public string getHeader()
        {
            return "ID" + ", " + "Title" + ", " + "ReleaseYear" + ", " + "Director" + ", " + "Genre" + ", " + "Rating";
        }

        public  string getString()
        {
            return this.ID.ToString() + ", " + this.Title.ToString().Replace(",", "") + ", " + this.ReleaseYear.ToString() + ", " + this.Director.ToString().Replace(",", "") + ", " +  this.Genre.ToString().Replace(",","") + ", " + this.Rating.ToString();
        }
    }
}
