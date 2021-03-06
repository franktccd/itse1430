﻿//Frank Rygiewicz
//ITSE-1430-21722
//2/20/2020

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharacterCreator.Winforms
{
    public partial class CharacterForm : Form
    {
        #region Ctors
        public CharacterForm ()
        {
            InitializeComponent();
        }

        public CharacterForm ( Character character ) : this(character!=null ? "Edit" : "Add", character) { }

        public CharacterForm (string name, Character character) : this()
        {
            Name = name;
            Character = character;
        }
        #endregion 
        public Character Character { get; set; }

        protected override void OnLoad ( EventArgs e )
        {
            base.OnLoad(e);

            var professions = Professions.GetAll();
            ddlProfession.Items.AddRange(professions);

            var races = Races.GetAll();
            ddlRace.Items.AddRange(races);

            if (Character != null)
            {
                txtName.Text = Character.Name;
                txtDescription.Text = Character.Description;
                txtStrength.Text = Character.Strength.ToString();
                txtAgility.Text = Character.Agility.ToString();
                txtConstitution.Text = Character.Constitution.ToString();
                txtIntelligence.Text = Character.Intelligence.ToString();
                txtWisdom.Text = Character.Wisdom.ToString();
                txtCharisma.Text = Character.Charisma.ToString();
                ddlProfession.Text = Character.Profession.Description;
                ddlRace.Text = Character.Race.Description;
            }
        }

        private Character GetCharacter()
        {
            var character = new Character();

            character.Name = txtName.Text?.Trim();
            character.Description = txtDescription.Text.Trim();
            character.Strength = GetAsInt32(txtStrength);
            character.Agility = GetAsInt32(txtAgility);
            character.Intelligence = GetAsInt32(txtIntelligence);
            character.Wisdom = GetAsInt32(txtWisdom);
            character.Charisma = GetAsInt32(txtCharisma);
            character.Constitution = GetAsInt32(txtConstitution);

            if (ddlProfession.SelectedItem is Profession profession)
                character.Profession = profession;

            if (ddlRace.SelectedItem is Race race)
                character.Race = race;

            return character;

        }
        #region Type Coercion
        private int GetAsInt32 ( Control control )
        {
            return GetAsInt32(control, 0);
        }

        private int GetAsInt32(Control control, int emptyValue)
        {
            if (String.IsNullOrEmpty(control.Text))
                return emptyValue;
            if (Int32.TryParse(control.Text, out var result))
                return result;
            return -1;
        }
        #endregion
        #region Event Handlers
        private void DisplayError (string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnOK ( object sender, EventArgs e )
        {
            var character = GetCharacter();
            var errors = ObjectValidator.Validate(character);
            if (errors.Any())
            {
                DisplayError("Error");
                return;
            };

            Character = character;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancel ( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
