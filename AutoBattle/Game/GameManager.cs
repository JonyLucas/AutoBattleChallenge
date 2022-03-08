using AutoBattle.Characters;
using AutoBattle.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Game
{
    public class GameManager
    {
        private static GameManager _instance;

        private readonly CharacterManager _characterManager;
        private readonly GridManager _gridManager;

        private int _currentTurn = 0;
        //private int numberOfPossibleTiles = grid.grids.Count;

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

            //AlocatePlayers();
            StartTurn();
        }

        private void StartTurn()
        {
            if (_currentTurn == 0)
            {
                //AllPlayers.Sort();
                _characterManager.AllPlayers.Shuffle();
            }

            var allPlayers = CharacterManager.AllPlayers;
            var grid = GridManager.Grid;

            foreach (Character character in allPlayers)
            {
                character.StartTurn(grid);
            }

            _currentTurn++;
            HandleTurn();
        }

        private void HandleTurn()
        {
            if (_characterManager.PlayerCharacter.Health == 0)
            {
                Console.WriteLine("Game Over");
                return;
            }
            else if (_characterManager.EnemyCharacter.Health == 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                // endgame?
                Console.WriteLine("You Win");

                Console.Write(Environment.NewLine + Environment.NewLine);

                return;
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