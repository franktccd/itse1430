using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Memory
{
    public class MemoryCharacterRoster : CharacterRoster
    {
        protected override Character AddCore ( Character character )
        {
            var item = CloneCharacter(character);
            item.Id = _id++;

            _characters.Add(item);

            return CloneCharacter(item);
        }

        protected override void DeleteCore ( int charID )
        {
            var character = FindById(charID);
            if (character != null)
                _characters.Remove(character);
        }

        protected override Character GetCore ( int charID )
        {
            var character = FindById(charID);
            if (character == null)
                return null;

            return CloneCharacter(character);
        }

        protected override IEnumerable<Character> GetAllCore ()
        {
            //Clone objects
            foreach(var character in _characters)
            {
                yield return CloneCharacter(character);
            };
        }

        protected override void UpdateCore ( int charID, Character newChar )
        {
            var existing = FindById(charID);

            CopyCharacter(existing, newChar, false);
        }

        private Character CloneCharacter(Character character)
        {
            var item = new Character();
            CopyCharacter(item, character, true);

            return item;
        }

        private void CopyCharacter(Character target, Character source, bool includeId)
        {
            if (includeId)
                target.Id = source.Id;
            target.Name = source.Name;
            target.Profession = source.Profession;
            target.Race = source.Race;
            target.Strength = source.Strength;
            target.Agility = source.Agility;
            target.Constitution = source.Constitution;
            target.Charisma = source.Charisma;
            target.Wisdom = source.Wisdom;
            target.Intelligence = source.Intelligence;

        }

        protected override Character FindByName(string name)
        {
            foreach(var character in _characters)
            {
                if (String.Compare(character?.Name, name, true) == 0)
                    return character;
            };

            return null;
        }

        protected override Character FindById(int id)
        {
            foreach(var character in _characters)
            {
                if (character.Id == id)
                    return character;
            };

            return null;
        }

        private readonly List<Character> _characters = new List<Character>();
        private int _id = 1;
    }
}
