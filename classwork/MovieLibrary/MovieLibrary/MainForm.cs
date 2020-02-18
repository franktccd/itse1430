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

        protected override void OnFormClosing ( FormClosingEventArgs e )
        {
            base.OnFormClosing(e);

            if (_movie != null)
                if (!DisplayConfirmation("Are you sure you want to close?", "Close"))
                    e.Cancel = true;
        }

        private void OnMovieAdd ( object sender, EventArgs e )
        {
            MovieForm child = new MovieForm();

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save the movie
            _movie = child.Movie;
            //child.ShowDialog(); - modal
            //child.Show(); //- modeless
        }

        private void OnMovieEdit ( object sender, EventArgs e )
        {
            if (_movie == null)
                return;

            var child = new MovieForm();
            child.Movie = _movie;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save the movie
            _movie = child.Movie;
            //child.ShowDialog(); - modal
            //child.Show(); //- modeless
        }

        private Movie _movie;

        private void OnMovieDelete ( object sender, EventArgs e )
        {
            //Verify movie
            if (_movie == null)
                return;

            if (!DisplayConfirmation($"Are you sure you want to delete {_movie.Title}?", "Delete"))
                return;

            //TODO: Delete
            _movie = null;
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