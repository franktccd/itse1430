//Frank Rygiewicz
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
using CharacterCreator.Memory;

namespace CharacterCreator.Winforms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            _characters = new MemoryCharacterRoster();
        }

        private bool DisplayConfirmation(string message, string character)
        {
            var result = MessageBox.Show(message, character, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            return result == DialogResult.OK;
        }

        private void DisplayError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void OnFileExit ( object sender, EventArgs e )
        {
            Close();
        }

        private void OnHelpAbout ( object sender, EventArgs e )
        {
            var about = new AboutBox1();

            about.ShowDialog(this);
        }

        private void OnCharacterNew ( object sender, EventArgs e )
        {
            CharacterForm newCharacter = new CharacterForm();

            do
            {
                if (newCharacter.ShowDialog(this) != DialogResult.OK)
                    return;
                try
                {
                    var character = _characters.Add(newCharacter.Character);
                    if (character != null)
                    {
                        UpdateUI();
                        return;
                    }
                } catch (Exception ex)
                {
                    DisplayError(ex.Message);
                } 
            } while (true);

            //if (newCharacter.ShowDialog(this) != DialogResult.OK)
            //    return;
            //_character = newCharacter.Character;
        }

        private void OnCharacterEdit ( object sender, EventArgs e )
        {
            var character = GetSelectedCharacter();
            if (character == null)
            {
                DisplayError("Please select a character to edit.");
                return;
            }
            var child = new CharacterForm();

            child.Character = character;
            do
            {
                if (child.ShowDialog(this) != DialogResult.OK)
                    return;
                try
                {
                    _characters.Update(character.Id, child.Character);
                    UpdateUI();
                    return;
                }
                catch (Exception ex)
                {
                    DisplayError(ex.Message);
                }
            } while (true);
        }

        private void OnCharacterDelete ( object sender, EventArgs e )
        {
            var character = GetSelectedCharacter();
            if (character == null)
            {
                DisplayError("There is no character to delete.");
                return;
            }
            if (!DisplayConfirmation($"Are you sure you want to delete {character.Name}?", "Delete"))
                return;
            try
            {
                _characters.Delete(character.Id);
                UpdateUI();
            } catch (Exception ex)
            {
                DisplayError(ex.Message);
            };
        }

        private readonly ICharacterRoster _characters;

        protected override void OnLoad ( EventArgs e )
        {
            base.OnLoad(e);

            UpdateUI();
        }

        private Character GetSelectedCharacter()
        {
            var SelectedItems = lstCharacters.SelectedItems.OfType<Character>();
            return SelectedItems.FirstOrDefault();
        }

        private void UpdateUI()
        {
            lstCharacters.Items.Clear();

            var characters = from character in _characters.GetAll()
                             where character.Id > 0
                             orderby character.Name descending
                             select character;

            lstCharacters.Items.AddRange(characters.ToArray());

        }
    }
}
