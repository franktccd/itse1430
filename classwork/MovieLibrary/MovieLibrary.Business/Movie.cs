﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business
{
    public class Movie : IValidatableObject
    {
        public Genre Genre { get; set; }

        /// <summary>Gets or sets the title.</summary>
        public string Title
        {
            // get {  return _title ?? "";  }
            get => _title ?? ""; // Expression body
            //set { _title = value?.Trim(); }
            set => _title = value?.Trim();
        }
        private string _title;

        /// <summary>Gets or sets the run length in minutes.</summary>
        //public int RunLength
        //{
        //    get { return _runLength;  }
        //    set { _runLength = value; }
        //}
        //private int _runLength;
        public int RunLength { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get => _description ?? "";
            set => _description = value?.Trim();
        }
        private string _description;

        /// <summary>Gets or sets the release year.</summary>
        /// <value>Default is 1900.</value>
        //public int ReleaseYear
        //{
        //    get { return _releaseYear; }
        //    set { _releaseYear = value; }
        //}
        //private int _releaseYear = 1900;
        public int ReleaseYear { get; set; } = 1900;

        /// <summary>Determines if this is a classic movie.</summary>
        //public bool IsClassic
        //{
        //    get { return _isClassic; }
        //    set { _isClassic = value; }
        //}
        //private bool _isClassic;
        public bool IsClassic { get; set; }

        public bool IsBlackAndWhite => ReleaseYear <= 1930;
        //{
        //    get => ReleaseYear <= 1930; 
        //}

        //public int Id
        //{
        //    get { return _id; }
        //    private set { _id = value; }
        //}
        //private int _id;
        public int Id { get; set; }

        public override string ToString () => Title;
        //{
        //    return Title;
        //}
        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            //Title is required
            //if(txtTitle.Text?.Trim() == "")
            if(String.IsNullOrEmpty(Title))
            {
                yield return new ValidationResult("Title is required.", new[] { nameof(Title) });
                //error = "Title is required.";
            }

            //Run length >= 0
            if (RunLength < 0)
            {
                yield return new ValidationResult("Run length must be >= 0.", new[] { nameof(RunLength) });
                //error = "Run length must be >= 0.";
            }

            //Release year >= 1900
            if (ReleaseYear < 1900)
            {
                yield return new ValidationResult("Release year must be >= 1900.", new[] { nameof(ReleaseYear) });
                //error = "Release year must be >= 1900.";
            }

            //error = null;
            //return true;
        }

        
    }

}
