using AutoBattle.Game;
using System;

namespace AutoBattle
{
    internal static class Program
    {
        private static GameManager gameManager;

        private static void Main(string[] args)
        {
            gameManager = GameManager.Instance;
            gameManager.GridManager.Grid = CreateGrid();

            CreaterPlayer();
            CreateEnemy();
            //CreateMultipleEnemies();

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

        /// <summary>
        /// Creates the player character and define its character class.
        /// </summary>
        private static void CreaterPlayer()
        {
            bool isValidChoice = false;

            while (!isValidChoice) // Utilize a loop structure instead of recursive calls for better performance
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out int indexCharacterClass) && indexCharacterClass <= 4 && indexCharacterClass >= 1)
                {
                    gameManager.CharacterManager.CreatePlayerCharacter(indexCharacterClass);
                    isValidChoice = true;
                }
            }
        }

        private static void CreateEnemy()
        {
            gameManager.CharacterManager.CreateEnemyCharacter();
        }

        /// <summary>
        /// This method create multiple enemies (without a especific team). (Not utilized yet)
        /// PS.: We could create another method to create the teams and its specific name, symbol (to appears different from the others) and number of characters.
        /// </summary>
        private static void CreateMultipleEnemies()
        {
            //This is the logic to create multiple enemy characters.
            //We could create another method to create the teams and its specific name, symbol (to appears different from the others) and number of characters.

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