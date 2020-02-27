using System;
using System.Windows.Forms;

using MovieLibrary.Business;
using MovieLibrary.WinForms;

namespace MovieLibrary
{
    public partial class MainForm : Form
    {
        public MainForm ()
        {
            InitializeComponent();

            #region Playing with objects

            //Full name
            //MovieLibrary.Business.Movie;
            //var movie = new Movie();

            //movie.Title = "Jaws";
            //movie.description = movie.Title;

            //movie = new Movie();

            //DisplayMovie(movie);
            //DisplayMovie(null);
            //DisplayConfirmation("Are you sure?", "Start");
            #endregion
        }

        private bool DisplayConfirmation ( string message, string title )
        {
            //Display a confirmation dialog
            var result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            //Return true if user selected OK
            return result == DialogResult.OK;
        }

        /// <summary>Displays an error message.</summary>
        /// <param name="message">Error to display.</param>
        private void DisplayError ( string message )
        {
            #region Playing with this

            //this represents the current instance
            //var that = this;

            //var Text = "";

            //These are equal
            //var newTitle = this.Text;
            //var newTitle = Text;
            #endregion

            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region Playing with methods

        void DisplayMovie ( Movie movie )
        {
            if (movie == null)
                return;

            var title = movie.Title;
            movie.Description = "Test";

            movie = new Movie();
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateUI();
        }

        //protected override void OnFormClosing ( FormClosingEventArgs e )
        //{
        //    base.OnFormClosing(e);

        //    if (_movie != null)
        //        if (!DisplayConfirmation("Are you sure you want to close?", "Close"))
        //            e.Cancel = true;
        //}

        private void OnMovieAdd ( object sender, EventArgs e )
        {
            MovieForm child = new MovieForm();

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save the movie
            AddMovieHelper(child.Movie);
            UpdateUI();
            //child.ShowDialog(); - modal
            //child.Show(); //- modeless
        }

        private void UpdateUI()
        {
            lstMovies.Items.Clear();
            var movies = GetMovies();
            foreach (var movie in movies)
            {
                if(movie != null)
                    lstMovies.Items.Add(movie);
            };
        }

        private void AddMovieHelper(Movie movie)
        {
            for(var index = 0; index < _movies.Length; ++index)
            {
                if(_movies[index] == null)
                {
                    _movies[index] = movie;
                    break;
                };
            };
        }

        private Movie[] GetMovies ()
        {
            return _movies;
        }

        private Movie GetSelectedMovie()
        {
            return lstMovies.SelectedItem as Movie;
        }

        private void UpdateMovie ( Movie oldMovie, Movie newMovie )
        {
            for (var index = 0; index < _movies.Length; ++index)
            {
                if (_movies[index] == oldMovie)
                {
                    _movies[index] = newMovie;
                    break;
                };
            };
        }

        private void DeleteMovie ( Movie movie )
        {
            for (var index = 0; index < _movies.Length; ++index)
            {
                if (_movies[index] == movie)
                {
                    _movies[index] = null;
                    break;
                };
            };
            UpdateUI();
        }

        private void OnMovieEdit ( object sender, EventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            var child = new MovieForm();
            child.Movie = movie;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save the movie
            UpdateMovie(movie, child.Movie);
            UpdateUI();
            //movie = child.Movie;
            //child.ShowDialog(); - modal
            //child.Show(); //- modeless
        }

        //private Movie _movie;
        private Movie[] _movies = new Movie[100];

        private void OnMovieDelete ( object sender, EventArgs e )
        {
            //Verify movie
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            if (!DisplayConfirmation($"Are you sure you want to delete {movie.Title}?", "Delete"))
                return;

            //TODO: Delete
            DeleteMovie(movie);
        }

        private void OnFileExit ( object sender, EventArgs e )
        {
            Close();
        }

        private void OnHelpAbout ( object sender, EventArgs e )
        {
            var about = new AboutBox();

            about.ShowDialog(this);
        }
    }
}