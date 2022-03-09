using AutoBattle.Game;
using System;

namespace AutoBattle
{
    internal class Program
    {
        private static GameManager gameManager;

        private static void Main(string[] args)
        {
            gameManager = GameManager.Instance;
            gameManager.GridManager.Grid = CreateGrid();

            GetPlayerChoice();
            GetEnemyQuantity();

            gameManager.StartGame();
        }

        private static Grid CreateGrid()
        {
            int columns = 0;
            int lines = 0;

            while (columns <= 1 || lines <= 1)
            {
                Console.WriteLine("Choose the grid's number of columns:");
                columns = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Choose the grid's number of lines:");
                lines = Int32.Parse(Console.ReadLine());
            }

            return new Grid(lines, columns);
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

        private static void GetEnemyQuantity()
        {
            Console.WriteLine("Choose the number of enemies in the game:");
            string input = Console.ReadLine();

            if (Int32.TryParse(input, out int enemyQuantity) && enemyQuantity > 0 && enemyQuantity < gameManager.GridManager.NumberOfPossibleTiles)
            {
                for (int i = 0; i < enemyQuantity; i++)
                {
                    gameManager.CharacterManager.CreateEnemyCharacter();
                }
            }
        }
    }
}