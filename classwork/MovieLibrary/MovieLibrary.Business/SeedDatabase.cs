using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public static class SeedDatabase
    {
        //Extension method
        public static IMovieDatabase SeedIfEmpty(this IMovieDatabase database)
        {
            if(database.GetAll().Any())
            {
                //Object Initializer
                var demo = new Movie() { Title = "Dune", RunLength = 260, ReleaseYear = 1985 };
                //Collection Initializer
                var items = new Movie[]
                    {
                        new Movie() {Title = "Jaws", RunLength = 120, ReleaseYear = 1977},
                        new Movie() { Title = "Jaws 2", RunLength = 120, ReleaseYear = 1977 },
                        demo,
                    };
                
            };

            return database;
        }
    }
}
