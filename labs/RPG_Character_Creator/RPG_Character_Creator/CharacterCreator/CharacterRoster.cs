using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator

   
{
    public abstract class CharacterRoster : ICharacterRoster
    {
        public Character Add ( Character character )
        {
            if (character == null)
                return null;

            var errors = ObjectValidator.Validate(character);
            if (errors.Any())
                return null;

            var existing = FindByName(character.Name);
            if (existing != null)
                return null;

            return AddCore(character);
        }

        protected abstract Character AddCore(Character character);

        public void Delete ( int charID )
        {
            //TODO: Validate
            if (charID <= 0)
                return;

            DeleteCore(charID);
        }

        protected abstract void DeleteCore(int id);

        public Character Get ( int charID )
        {
            //TODO: Validate
            if (charID <= 0)
                return null;

            return GetCore(charID);
        }

        protected abstract Character GetCore(int id);

        public IEnumerable<Character> GetAll ()
        {
            return GetAllCore();
        }

        protected abstract IEnumerable<Character> GetAllCore();

        public string Update ( int charID, Character newChar )
        {
            //TODO: Validate
            if (newChar == null)
                return "Character is null";
            var errors = ObjectValidator.Validate(newChar);
            if(errors.Any())
                return "Error";
            if (charID <= 0)
                return "ID is invalid";

            var existing = FindById(charID);
            if (existing == null)
                return "Character not found";

            var sameName = FindByName(newChar.Name);
            if (sameName != null && sameName.Id != charID)
                return "Character name must be unique";

            UpdateCore( charID, newChar);

            return null;
        }

        protected abstract void UpdateCore(int id, Character character);


        protected abstract Character FindByName(string name);

        protected abstract Character FindById(int id);
    }
}
