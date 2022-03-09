using AutoBattle.Characters;
using AutoBattle.Extensions;
using System;

namespace AutoBattle.Game
{
    /// <summary>
    /// GameManager is a singleton responsible to hold the main logic for the main game loop and provide access to the CharacterManager and GridManager.
    /// </summary>
    public class GameManager
    {
        private static GameManager _instance;

        private readonly CharacterManager _characterManager;
        private readonly GridManager _gridManager;
        private int _currentTurn = 0;

        // Get the singleton instance
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public CharacterManager CharacterManager
        { get { return _characterManager; } }

        public GridManager GridManager
        { get { return _gridManager; } }

        private GameManager()
        {
            _characterManager = new CharacterManager();
            _gridManager = new GridManager();
        }

        public void StartGame()
        {
            //populates the character variables and targets
            var playerCharacter = _characterManager.PlayerCharacter;
            var enemyCharacter = _characterManager.EnemyCharacter;
            _characterManager.SetTarget(playerCharacter, enemyCharacter);
            _characterManager.SetTarget(enemyCharacter, playerCharacter);

            _gridManager.AlocateCharacterLocation(playerCharacter);
            _gridManager.AlocateEnemyCharacter(enemyCharacter);

            StartTurn();
        }

        private void StartTurn()
        {
            if (_currentTurn == 0)
            {
                // Shuffles the Characters list to randomize the first character to play
                _characterManager.AllPlayers.Shuffle();
            }

            var allPlayers = CharacterManager.AllPlayers;

            foreach (Character character in allPlayers)
            {
                character.StartTurn();
            }

            _currentTurn++;
            HandleTurn();
        }

        private void HandleTurn()
        {
            if (_characterManager.PlayerCharacter.Health <= 0)
            {
                Console.WriteLine("Game Over");
            }
            else if (_characterManager.EnemyCharacter.Health <= 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                Console.WriteLine("You Win");

                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");
                Console.Write(Environment.NewLine + Environment.NewLine);

                ConsoleKeyInfo key = Console.ReadKey();
                StartTurn();
            }
        }
    }
}