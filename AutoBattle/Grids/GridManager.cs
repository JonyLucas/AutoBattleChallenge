using System;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class GridManager
    {
        public Grid Grid { get; set; }

        public void AlocateCharacterLocation(Character character)
        {
            int random = 0;
            GridBox RandomLocation = (Grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                RandomLocation.ocupied = true;
                Grid.grids[random] = RandomLocation;
                character.currentBox = Grid.grids[random];
            }
            else
            {
            }
        }

        public void AlocateEnemyCharacter(Character character)
        {
            int random = 24;
            GridBox RandomLocation = (Grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                RandomLocation.ocupied = true;
                Grid.grids[random] = RandomLocation;
                character.currentBox = Grid.grids[random];
                Grid.drawBattlefield(5, 5);
            }
            else
            {
                //AlocateEnemyCharacter();
            }
        }
    }
}