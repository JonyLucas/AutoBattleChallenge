using AutoBattle.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    /// <summary>
    /// Character Manager holds the character instances and also the initialization of those character.
    /// </summary>
    public class CharacterManager
    {
        private Character _playerCharacter;
        private Character _enemyCharacter;
        private readonly List<Character> _allPlayers = new List<Character>();

        public List<Character> AllPlayers
        { get { return _allPlayers; } }

        public Character PlayerCharacter // It's temporary, we could only use the AllPlayers property to manipulate the characters
        { get { return _playerCharacter; } }

        public Character EnemyCharacter // It's temporary, we could only use the AllPlayers property to manipulate the characters
        { get { return _enemyCharacter; } }

        public void CreatePlayerCharacter(int classOption)
        {
            CharacterClass characterClass = (CharacterClass)classOption;
            Console.WriteLine($"Player Class Choice: {characterClass}");
            _playerCharacter = CharacterFactory.CreateCharacter(characterClass, 0, true);
            _allPlayers.Add(_playerCharacter);
        }

        public void CreateEnemyCharacter()
        {
            var rand = new Random();
            int randomInteger = rand.Next(1, 4);
            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");

            var index = _allPlayers.Count;
            _enemyCharacter = CharacterFactory.CreateCharacter(enemyClass, index, false);
            _allPlayers.Add(_enemyCharacter);
        }

        public void SetTarget(Character character, Character target)
        {
            character.Target = target;
        }

        public Character GetCharacterInGridBox(GridBox gridBox)
        {
            return _allPlayers.FirstOrDefault(character => character.currentBox.Index == gridBox.Index);
        }
    }
}