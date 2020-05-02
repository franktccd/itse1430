//Frank Rygiewicz
//ITSE-1430-21722
//2/20/2020

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class Character : IValidatableObject
    {

        public int Id { get; set; }

        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value?.Trim(); }
        }
        private string _name;

        public Race Race { get; set; }

        public Profession Profession { get; set; }

        public int Strength { get; set; } = 50;
        public int Agility { get; set; } = 50;
        public int Constitution { get; set; } = 50;
        public int Intelligence { get; set; } = 50;
        public int Wisdom { get; set; } = 50;
        public int Charisma { get; set; } = 50;


        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value?.Trim(); }
        }
        private string _description;

        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            if (String.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required.", new[] { nameof(Name)});
            }

            if(Race == null)
            {
                yield return new ValidationResult("Race is required.", new[] { nameof(Race) });
            }

            if (Profession == null)
            {
                yield return new ValidationResult("Profession is required.", new[] { nameof(Profession) });
            }

            if (Strength < 1 || Strength > 100)
            {
                yield return new ValidationResult("Strength must be between 1 and 100.", new[] { nameof(Strength) });
            }
            if (Agility < 1 || Agility > 100)
            {
                yield return new ValidationResult("Agility must be between 1 and 100.", new[] { nameof(Agility) });
            }
            if (Constitution < 1 || Constitution > 100)
            {
                yield return new ValidationResult("Constituition must be between 1 and 100.", new[] { nameof(Constitution) });
            }
            if (Intelligence < 1 || Intelligence > 100)
            {
                yield return new ValidationResult("Intelligence must be between 1 and 100.", new[] { nameof(Intelligence) });
            }
            if (Wisdom < 1 || Wisdom > 100)
            {
                yield return new ValidationResult("Wisdom must be between 1 and 100.", new[] { nameof(Wisdom) });
            }
            if (Charisma < 1 || Charisma > 100)
            {
                yield return new ValidationResult("Charisma must be between 1 and 100.", new[] { nameof(Charisma) });
            }
        }


    }

    #region Race Class
    public class Race
    {
        public Race ( string description )
        {
            Description = description ?? "Value could not be transferred";
        }

        public string Description { get; set; }

        public override string ToString ()
        {
            return Description;
        }
    }
    public class Races
    {
        public static Race[] GetAll ()
        {
            var items = new Race[5];
            items[0] = new Race("Human");
            items[1] = new Race("Elf");
            items[2] = new Race("Dwarf");
            items[3] = new Race("Gnome");
            items[4] = new Race("Half Elf");

            return items;
        }
    }

    #endregion

    #region Profession Class
    public class Profession
    {
        public Profession ( string description )
        {
            Description = description ?? "Value could not be transferred";
        }

        public string Description { get; set; }

        public override string ToString ()
        {
            return Description;
        }
    }
    public class Professions
    {
        public static Profession[] GetAll ()
        {
            var items = new Profession[5];
            items[0] = new Profession("Fighter");
            items[1] = new Profession("Hunter");
            items[2] = new Profession("Rogue");
            items[3] = new Profession("Priest");
            items[4] = new Profession("Wizard");

            return items;
        }
    }
    #endregion

    
}
