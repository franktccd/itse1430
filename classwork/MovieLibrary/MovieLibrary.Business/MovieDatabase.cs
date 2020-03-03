using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    //Responsible for create, add, update, delete
    public class MovieDatabase
    {
        public Movie Add ( Movie movie )
        {
            //Validate
            if (movie == null)
                return null;
            if (!movie.Validate(out var error))
                return null;

            //Movie names must be unique
            var existing = FindByTitle(movie.Title);
            if (existing != null)
                return null;

            //Clone movie to store     
            var item = CloneMovie(movie);
            item.Id = _id++;
            _movies.Add(item);
            //for (var index = 0; index < _movies.Count; ++index)
            //{
            //    if (_movies[index] == null)
            //    {
            //        _movies[index] = item;
            //        item.Id = _id++;

            //        return CloneMovie(item);
            //    };
            //};

            //return null;
            return CloneMovie(item);
        }

        public void Delete ( int id )
        {
            //Validate
            if (id <= 0)
                return;

            //Better way to find movie
            var movie = FindById(id);
            if (movie != null)
                _movies.Remove(movie);
            //for (var index = 0; index < _movies.Count; ++index)
            //{
            //    if (_movies[index]?.Id == id)
            //    {
            //        _movies[index] = null;
            //        return;
            //    };
            //};
        }

        public Movie[] GetAll ()
        {
            //Clone objects   
            var items = new Movie[_movies.Count];
            var index = 0;
            foreach(var movie in _movies)
            {
                items[index++] = CloneMovie(movie);
            }
            return items;
        }

        public void Update ( int id, Movie newMovie )
        {

            //TODO: Validate
            //TODO: Movie names must be unique
            //TODO: Clone movie to store
            //TODO: Return new movie
            //TODO: Shouldn't need the original movie
            for (var index = 0; index < _movies.Count; ++index)
            {
                if (_movies[index]?.Id == id)
                {
                    _movies[index] = newMovie;
                    break;
                };
            };
        }

        private Movie FindByTitle(string title)
        {
            foreach(var movie in _movies)
            {
                if (String.Compare(movie?.Title, title, true) == 0)
                    return movie;
            };

            return null;
        }

        private Movie CloneMovie(Movie movie)
        {
            //Object initializer syntax
            return new Movie() 
            {
                Id = movie.Id, Title = movie.Title,
                Description = movie.Description,
                Genre = new Genre(movie.Genre.Description),
                IsClassic = movie.IsClassic,
                ReleaseYear = movie.ReleaseYear,
                RunLength = movie.RunLength,
            };
            //item.Id = movie.Id;
            //item.Title = movie.Title;
            //item.Description = movie.Description;
            //item.Genre = movie.Genre;
            //item.IsClassic = movie.IsClassic;
            //item.ReleaseYear = movie.ReleaseYear;
            //item.RunLength = movie.RunLength;

            //return item;
        }

        private Movie FindById(int id)
        {
            foreach (var movie in _movies)
            {
                if (movie.Id == id)
                    return movie;
            };

            return null;
        }

        //private readonly Movie[] _movies = new Movie[100];
        private readonly List<Movie> _movies = new List<Movie>();
        private int _id = 1;
    }
}
