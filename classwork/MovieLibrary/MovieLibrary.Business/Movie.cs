using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public class Movie
    {
        public string Title
        {
            get { return _title;  }
            set { _title = value; }
        }
        private string _title;

        public int runLength;

        public string description;

        public int releaseYear;

        public bool isClassic;        
    }
}
