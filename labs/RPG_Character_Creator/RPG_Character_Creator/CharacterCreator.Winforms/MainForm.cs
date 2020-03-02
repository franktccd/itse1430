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

namespace CharacterCreator.Winforms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_character != null)
                if (!DisplayConfirmation("Are you sure you want to close?", "Close"))
                    e.Cancel = true;
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

            if (newCharacter.ShowDialog(this) != DialogResult.OK)
                return;
            _character = newCharacter.Character;
        }

        private Character _character;

        private void OnCharacterEdit ( object sender, EventArgs e )
        {
            if (_character == null)
            {
                DisplayError("You need to create a new character before editing.");
                return;
            }
            var child = new CharacterForm();

            child.Character = _character;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;
            _character = child.Character;
        }

        private void OnCharacterDelete ( object sender, EventArgs e )
        {
            if (_character == null)
            {
                DisplayError("There is no character to delete.");
                return;
            }
            if (!DisplayConfirmation("Are you sure you want to delete your character?", "Delete"))
                return;
            _character = null;
        }
    }
}
