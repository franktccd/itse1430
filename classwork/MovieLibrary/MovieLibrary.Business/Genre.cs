using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public class Genre
    {
        public Genre( string description)
        {
            Description = description ?? "";
        }
        public string Description { get; }

        public override string ToString ()
        {
            return Description;
        }
    }

    public class Genres
    {
        public static Genre[] GetAll()
        {
            var items = new Genre[5];
            items[0] = new Genre("Action");
            items[1] = new Genre("Comedy");
            items[2] = new Genre("Drama");
            items[3] = new Genre("Horror");
            items[4] = new Genre("Rom-Com");

            return items;
        }
    }
}
