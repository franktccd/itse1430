using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public interface ICharacterRoster
    {
        Character Add ( Character character );

        void Delete ( int charID );

        Character Get ( int charID );

        IEnumerable<Character> GetAll ();

        string Update ( int charID, Character newChar );
    }
}
