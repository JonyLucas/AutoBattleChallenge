using AutoBattle.Characters;
using AutoBattle.Game;
using System;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    /// <summary>
    /// GridManager holds the reference of the grid and logic related to the grid manipulation.
    /// </summary>
    public class GridManager
    {
        public Grid Grid { get; set; }

        public int NumberOfPossibleTiles
        { get { return Grid.grids.Count; } }

        public void AlocateCharacterLocation(Character character)
        {
            bool isPositionated = false;

            while (!isPositionated)
            {
                Random rand = new Random();
                int random = rand.Next(0, NumberOfPossibleTiles);
                GridBox randomLocation = (Grid.grids.ElementAt(random));

                if (!randomLocation.occupied)
                {
                    isPositionated = true;
                    randomLocation.occupied = true;
                    Grid.grids[random] = randomLocation;
                    character.currentBox = randomLocation;

                    Console.WriteLine($"Player {character.PlayerIndex} is positioned at ({randomLocation.xIndex}, {randomLocation.yIndex}) \n");
                    DrawBattlefield();
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            var characterManager = GameManager.Instance.CharacterManager;
            var lines = Grid.YLength;
            var columns = Grid.XLenght;

            int index = 0;
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    GridBox currentgrid = Grid.GetGridBoxByIndex(index++);
                    var character = characterManager.GetCharacterInGridBox(currentgrid);

                    if (currentgrid.occupied)
                    {
                        // O represents the player's character, and the X the enemy. It was done so we could differentiate  them from each other.
                        Console.Write($"[{(character.IsPlayer ? 'O' : 'X')}]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
}