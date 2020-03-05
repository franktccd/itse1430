using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public class SeedDatabase
    {
        public IMovieDatabase SeedIfEmpty(IMovieDatabase database)
        {
            if(database.GetAll().Length == 0)
            {
                database.Add(new Movie() {Title = "Jaws", RunLength = 120, ReleaseYear = 1977});
                database.Add(new Movie() { Title = "Jaws 2", RunLength = 120, ReleaseYear = 1977 });
                database.Add(new Movie() { Title = "Star Wars", RunLength = 120, ReleaseYear = 1977 });
                database.Add(new Movie() { Title = "Tremors", RunLength = 120, ReleaseYear = 1977 });
            };

            return database;
        }
    }
}
