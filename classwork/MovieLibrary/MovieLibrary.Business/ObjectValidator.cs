using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Business
{
    //public interface ISelectableObject
    //{
    //    void Select ();
    //}

    //public interface IResizableObject
    //{
    //    void Resize ( int width, int height );
    //}

    //public class SelectableResizableObject : ISelectableObject, IResizableObject
    //{
    //    public void Resize ( int width, int height );
    //    public void Select ();
    //}

    //Responsible for create, add, update, delete

    public static class ObjectValidator
    {
        //Global functions
        public static IEnumerable<ValidationResult> Validate (object value)
        {
            //.NET Validation
            var errors = new List<ValidationResult>();
            Validator.TryValidateObject(value, new ValidationContext(value), errors, true);

            return errors;
        }
    }
}
