using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    public class CharacterFactory
    {
        public static Character CreateCharacter(int classIndex, int index)
        {
            CharacterClass characterClass = (CharacterClass)classIndex;
            Console.WriteLine($"Player Class Choice: {characterClass}");
            var character = new Character(characterClass);
            character.Health = 100;
            character.BaseDamage = 20;
            character.PlayerIndex = index;

            return character;
        }
    }
}