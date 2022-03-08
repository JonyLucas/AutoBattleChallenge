using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    public class CharacterManager
    {
        private Character _playerCharacter;
        private Character _enemyCharacter;
        private readonly List<Character> _allPlayers = new List<Character>();

        public void CreatePlayerCharacter(int classOption)
        {
            _playerCharacter = CharacterFactory.CreateCharacter(classOption, 0);
            _allPlayers.Add(_playerCharacter);
        }

        public void CreateEnemyCharacter()
        {
            var rand = new Random();
            int randomInteger = rand.Next(1, 4);
            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");

            var index = _allPlayers.Count;
            _enemyCharacter = CharacterFactory.CreateCharacter(randomInteger, index);
            _allPlayers.Add(_enemyCharacter);
        }

        public void SetTarget(Character character, Character target)
        {
            character.Target = target;
        }
    }
}