﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class Character
    {
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

        public bool Validate ( out string error )
        {
            if (String.IsNullOrEmpty(Name))
            {
                error = "Name is required.";
                return false;
            }

            if (Strength < 0 || Strength > 100)
            {
                error = "Attribute must be between 0 and 100";
                return false;
            }
            if (Agility < 0 || Agility > 100)
            {
                error = "Attribute must be between 0 and 100";
                return false;
            }
            if (Constitution < 0 || Constitution > 100)
            {
                error = "Attribute must be between 0 and 100";
                return false;
            }
            if (Intelligence < 0 || Intelligence > 100)
            {
                error = "Attribute must be between 0 and 100";
                return false;
            }
            if (Wisdom < 0 || Wisdom > 100)
            {
                error = "Attribute must be between 0 and 100";
                return false;
            }
            if (Charisma < 0 || Charisma > 100)
            {
                error = "Attribute must be between 0 and 100";
                return false;
            }

            error = null;
            return true;
        }
    }

    #region Race Class
    public class Race
    {
        public Race ( string description )
        {
            Description = description ?? "";
        }

        public string Description { get; }

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
            Description = description ?? "";
        }

        public string Description { get; }

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
