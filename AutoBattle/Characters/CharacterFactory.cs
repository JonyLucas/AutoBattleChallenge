using AutoBattle.Enum;

namespace AutoBattle.Characters
{
    /// <summary>
    /// CharacterFactory holds the base logic to create an instance of Character and initialize its fields.
    /// </summary>
    public static class CharacterFactory
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