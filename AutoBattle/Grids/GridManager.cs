using AutoBattle.Game;
using System;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
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
                GridBox RandomLocation = (Grid.grids.ElementAt(random));

                if (!RandomLocation.occupied)
                {
                    isPositionated = true;
                    RandomLocation.occupied = true;
                    Grid.grids[random] = RandomLocation;
                    character.currentBox = Grid.grids[random];
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