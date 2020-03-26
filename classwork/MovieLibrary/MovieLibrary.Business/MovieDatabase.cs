using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Business
{
    public abstract class MovieDatabase : IMovieDatabase
    {
        public Movie Add ( Movie movie )
        {
            //Validate
            if (movie == null)
                return null;

            //.NET Validation
            var errors = ObjectValidator.Validate(movie);
            if (errors.Any())
            //if(!Validator.TryValidateObject(movie, new ValidationContext(movie), errors, true))
            //if (!movie.Validate(out var error))
                return null;

            //Movie names must be unique
            var existing = FindByTitle(movie.Title);
            if (existing != null)
                return null;

            return AddCore(movie);
        }

        protected abstract Movie AddCore ( Movie movie );

        public void Delete ( int id )
        {
            //Validate
            if (id <= 0)
                return;

            DeleteCore(id);
        }

        protected abstract void DeleteCore ( int id );

        public Movie Get( int id )
        {
            if (id <= 0)
                return null;

            return GetCore(id);
        }

        protected abstract Movie GetCore ( int id );

        public IEnumerable<Movie> GetAll ()
        {
            return GetAllCore();
        }

        protected abstract IEnumerable<Movie> GetAllCore ();

        public string Update ( int id, Movie movie )
        {

            //TODO: Validate
            if (movie == null)
                return "Movie is null";
            var errors = ObjectValidator.Validate(movie);
            if (errors.Any())
                return "Error";
            if (id <= 0)
                return "Id is invalid";

            var existing = FindById(id);
            if (existing == null)
                return "Movie not found";

            //Movie names must be unique
            var sameName = FindByTitle(movie.Title);
            if (sameName != null && sameName.Id != id)
                return "Movie must be unique";

            UpdateCore(id, movie);
            
            return null;
        }

        protected abstract void UpdateCore ( int id, Movie movie );

        protected abstract Movie FindByTitle ( string title );

        protected abstract Movie FindById ( int id );

    }
}
