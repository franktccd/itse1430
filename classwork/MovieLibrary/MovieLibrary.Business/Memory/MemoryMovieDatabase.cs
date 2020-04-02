using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace MovieLibrary.Business.Memory
{
    public class MemoryMovieDatabase : MovieDatabase
    {
        protected override Movie AddCore ( Movie movie )
        { 
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

        protected override void DeleteCore ( int id )
        {
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

        protected override Movie GetCore( int id )
        {
            var movie = FindById(id);
            if (movie == null)
                return null;

            return CloneMovie(movie);
        }

        protected override IEnumerable<Movie> GetAllCore ()
        {

            //Filtering
            var items = _movies.Where(m => true);

            //Transforms
            return _movies.Select(m => CloneMovie(m));

            
            //return _movies;
            //Clone objects   
            //var items = new Movie[_movies.Count];
            //var index = 0;
            //foreach (var movie in _movies)
            //{
            //    items[index++] = CloneMovie(movie);
            //}
            //return items;

            //Debug.WriteLine("Starting GetAllCore");

            //foreach(var movie in _movies)
            //{
            //    Debug.WriteLine($"Returning {movie.Id}");
            //    yield return CloneMovie(movie);
            //    Debug.WriteLine($"Returned {movie.Id}");
            //};
        }

        protected override void UpdateCore ( int id, Movie movie )
        {
            var existing = FindById(id);
            
            //Update
            CopyMovie(existing, movie, false);
        }

        private void CopyMovie ( Movie target, Movie source, bool includeID )
        {
            if (includeID)
                target.Id = source.Id;
            target.Title = source.Title;
            target.Description = source.Description;
            if (source.Genre != null)
                target.Genre = new Genre(source.Genre.Description);
            else target.Genre = null;
            target.IsClassic = source.IsClassic;
            target.ReleaseYear = source.ReleaseYear;
            target.RunLength = source.RunLength;
        }

        //Example of more complex querying with programmatic filters
        private IEnumerable<Movie> Query( string title, int releaseYear )
        {
            var query = from movie in _movies
                        select movie;
            if (!String.IsNullOrEmpty(title))
                query = query.Where(m => String.Compare(m.Title, title, true) == 0);

            if (releaseYear > 0)
                query = query.Where(m => m.ReleaseYear >= releaseYear);

            return query.ToList();
        }

        protected override Movie FindByTitle ( string title ) => (from movie in _movies 
                                                                  where String.Compare(movie.Title, title, true) == 0
                                                                  select movie).FirstOrDefault();
        //Expression Body -//=> _movies.FirstOrDefault(m => String.Compare(m?.Title, title, true) == 0);
        //{
        //    foreach (var movie in _movies)
        //    {
        //        if (String.Compare(movie?.Title, title, true) == 0)
        //            return movie;
        //    };

        //    return null;
        //}

        private Movie CloneMovie ( Movie movie )
        {
            var item = new Movie();
            CopyMovie(item, movie, true);
            return item;
            //Object initializer syntax
            //return new Movie() 
            //{
            //    Id = movie.Id, Title = movie.Title,
            //    Description = movie.Description,
            //    Genre = new Genre(movie.Genre.Description),
            //    IsClassic = movie.IsClassic,
            //    ReleaseYear = movie.ReleaseYear,
            //    RunLength = movie.RunLength,
            //};
            //item.Id = movie.Id;
            //item.Title = movie.Title;
            //item.Description = movie.Description;
            //item.Genre = movie.Genre;
            //item.IsClassic = movie.IsClassic;
            //item.ReleaseYear = movie.ReleaseYear;
            //item.RunLength = movie.RunLength;

            //return item;
        }

        //private bool IsId ( Movie movie ) => movie.Id == id;
        protected override Movie FindById ( int id ) =>_movies.FirstOrDefault(m => m.Id == id);
        //{
        //    _movies.FirstOrDefault(m => m.Id == id);

        //    foreach (var movie in _movies)
        //    {
        //        if (movie.Id == id)
        //            return movie;
        //    };

        //    return null;
        //}

        //private readonly Movie[] _movies = new Movie[100];
        private readonly List<Movie> _movies = new List<Movie>();
        private int _id = 1;
    }
}
