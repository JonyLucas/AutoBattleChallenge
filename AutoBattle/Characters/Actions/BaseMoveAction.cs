using AutoBattle.Enum;
using AutoBattle.Game;
using AutoBattle.Interfaces;
using System;

namespace AutoBattle.Characters.Actions
{
    public class BaseMoveAction : IMoveAction
    {
        public void ExecuteAction(Character character, Grid battlefield)
        {
            var direction = GetClosestDistanceComponent(character);
            var nextIndex = -1;

            // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
            if (direction == CharacterDirection.Left)
            {
                nextIndex = character.currentBox.Index - 1;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    MoveDirection(character, battlefield, nextIndex);
                    Console.WriteLine($"Player {character.PlayerIndex} walked left\n");
                }
            }
            else if (direction == CharacterDirection.Right)
            {
                nextIndex = character.currentBox.Index + 1;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    MoveDirection(character, battlefield, nextIndex);
                    Console.WriteLine($"Player {character.PlayerIndex} walked right\n");
                }
            }
            else if (direction == CharacterDirection.Up)
            {
                nextIndex = character.currentBox.Index - battlefield.YLength;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    MoveDirection(character, battlefield, nextIndex);
                    Console.WriteLine($"Player {character.PlayerIndex} walked up\n");
                }
            }
            else if (direction == CharacterDirection.Down)
            {
                nextIndex = character.currentBox.Index + battlefield.YLength;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    MoveDirection(character, battlefield, nextIndex);
                    Console.WriteLine($"Player {character.PlayerIndex} walked down\n");
                }
            }
        }

        private CharacterDirection GetClosestDistanceComponent(Character character)
        {
            var xDistance = Math.Abs(character.currentBox.xIndex - character.Target.currentBox.xIndex);
            var yDistance = Math.Abs(character.currentBox.yIndex - character.Target.currentBox.yIndex);

            if (xDistance == 0)
            {
                return character.currentBox.yIndex > character.Target.currentBox.yIndex ? CharacterDirection.Up : CharacterDirection.Down;
            }
            else if (yDistance == 0)
            {
                return character.currentBox.xIndex > character.Target.currentBox.xIndex ? CharacterDirection.Left : CharacterDirection.Right;
            }

            if (xDistance < yDistance)
            {
                // if the character x index is higher, it means that the character is positioned on the right of its target, so it must move left, and vice versa.
                return character.currentBox.xIndex > character.Target.currentBox.xIndex ? CharacterDirection.Left : CharacterDirection.Right;
            }
            else
            {
                // if the character y index is lower, it means that the character is positioned above its target, so it must move down, and vice versa.
                return character.currentBox.yIndex > character.Target.currentBox.yIndex ? CharacterDirection.Up : CharacterDirection.Down;
            }
        }

        // Moves the character for a new position and then redraw the grid
        private void MoveDirection(Character character, Grid battlefield, int nextIndex)
        {
            var nextBox = (battlefield.grids.Find(x => x.Index == nextIndex));
            var currentBox = character.currentBox;
            if (nextBox.occupied)
            {
                return;
            }

            currentBox.occupied = false;
            battlefield.grids[currentBox.Index] = currentBox;

            nextBox.occupied = true;
            character.currentBox = nextBox;
            battlefield.grids[nextBox.Index] = nextBox;

            GameManager.Instance.GridManager.DrawBattlefield();
        }

        // Checks in x and y directions if there is any character close enough to be a target.
        public bool CheckCloseTargets(Character character, Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == character.currentBox.Index - 1).occupied);
            bool right = (battlefield.grids.Find(x => x.Index == character.currentBox.Index + 1).occupied);
            bool up = (battlefield.grids.Find(x => x.Index == character.currentBox.Index + battlefield.YLength).occupied);
            bool down = (battlefield.grids.Find(x => x.Index == character.currentBox.Index - battlefield.YLength).occupied);

            if (left || right || up || down)
            {
                return true;
            }
            return false;
        }
    }
}