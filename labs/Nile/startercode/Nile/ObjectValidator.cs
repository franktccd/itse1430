/*
 * ITSE 1430
 * Frank Rygiewicz
 * 4/21/20
 */
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public static class ObjectValidator
{
    public static IEnumerable<ValidationResult> TryValidate (object value)
    {
        var errors = new List<ValidationResult>();

        Validator.TryValidateObject(value, new ValidationContext(value), errors, true) ;

        return errors;
    }

    public static void Validate (object value)
    {
        Validator.ValidateObject(value, new ValidationContext(value), true);
    }
}
