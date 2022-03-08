using AutoBattle.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    public class CharacterFactory
    {
        public static Character CreateCharacter(CharacterClass characterClass, int index, bool isPlayer)
        {
            var character = new Character(characterClass, isPlayer);
            character.Health = 100;
            character.BaseDamage = 20;
            character.PlayerIndex = index;

            return character;
        }
    }
}