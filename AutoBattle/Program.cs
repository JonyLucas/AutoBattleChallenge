using AutoBattle.Enum;
using AutoBattle.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    internal class Program
    {
        private static GameManager gameManager;

        private static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);

            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;
            gameManager = GameManager.Instance;
            Setup();

            void Setup()
            {
                GetPlayerChoice();
                CreateEnemyCharacter();
                StartGame();
            }

            void CreateEnemyCharacter()
            {
                gameManager.CharacterManager.CreateEnemyCharacter();
            }

            void StartGame()
            {
                PlayerCharacter = gameManager.CharacterManager.PlayerCharacter;
                EnemyCharacter = gameManager.CharacterManager.EnemyCharacter;
                gameManager.CharacterManager.SetTarget(PlayerCharacter, EnemyCharacter);
                gameManager.CharacterManager.SetTarget(EnemyCharacter, PlayerCharacter);
                //populates the character variables and targets
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void StartTurn()
            {
                if (currentTurn == 0)
                {
                    //AllPlayers.Sort();
                }

                foreach (Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if (PlayerCharacter.Health == 0)
                {
                    return;
                }
                else if (EnemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

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

            void AlocatePlayers()
            {
                //AlocatePlayerCharacter();
            }

            //void AlocatePlayerCharacter()
            //{
            //    int random = 0;
            //    GridBox RandomLocation = (grid.grids.ElementAt(random));
            //    Console.Write($"{random}\n");
            //    if (!RandomLocation.ocupied)
            //    {
            //        GridBox PlayerCurrentLocation = RandomLocation;
            //        RandomLocation.ocupied = true;
            //        grid.grids[random] = RandomLocation;
            //        PlayerCharacter.currentBox = grid.grids[random];
            //        AlocateEnemyCharacter();
            //    }
            //    else
            //    {
            //        AlocatePlayerCharacter();
            //    }
            //}

            //void AlocateEnemyCharacter()
            //{
            //    int random = 24;
            //    GridBox RandomLocation = (grid.grids.ElementAt(random));
            //    Console.Write($"{random}\n");
            //    if (!RandomLocation.ocupied)
            //    {
            //        EnemyCurrentLocation = RandomLocation;
            //        RandomLocation.ocupied = true;
            //        grid.grids[random] = RandomLocation;
            //        EnemyCharacter.currentBox = grid.grids[random];
            //        grid.drawBattlefield(5, 5);
            //    }
            //    else
            //    {
            //        AlocateEnemyCharacter();
            //    }
            //}
        }

        private static void GetGridSize()
        {
            Console.WriteLine("Choose the width of the grid:");
            string width = Console.ReadLine();

            Console.WriteLine("Choose the height of the grid:");
            string height = Console.ReadLine();
        }

        private static void GetPlayerChoice()
        {
            //asks for the player to choose between for possible classes via console.
            Console.WriteLine("Choose Between One of this Classes:\n");
            Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
            //store the player choice in a variable
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int indexCharacterClass) && indexCharacterClass <= 4 && indexCharacterClass >= 1)
            {
                gameManager.CharacterManager.CreatePlayerCharacter(indexCharacterClass);
            }
            else
            {
                GetPlayerChoice();
            }
        }
    }
}