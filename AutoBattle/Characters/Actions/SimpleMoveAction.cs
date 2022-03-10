using AutoBattle.Enum;
using AutoBattle.Game;
using AutoBattle.Interfaces;
using System;
using static AutoBattle.Types;

namespace AutoBattle.Characters.Actions
{
    /// <summary>
    /// This class implements a basic logic for the character's movement, where it only can move to one direction and one cell at each turn.
    /// </summary>
    public class SimpleMoveAction : IMoveAction
    {
        private readonly Character _character;
        private GridBox _previouPosition;

        public SimpleMoveAction(Character character)
        {
            _character = character;
        }

        public void ExecuteAction(Grid battlefield)
        {
            var direction = GetClosestDistanceComponent();
            var nextIndex = -1;

            // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
            if (direction == CharacterDirection.Left)
            {
                nextIndex = _character.currentBox.Index - 1;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    Console.WriteLine($"Player {_character.PlayerIndex} walked left\n");
                    MoveDirection(battlefield, nextIndex);
                }
            }
            else if (direction == CharacterDirection.Right)
            {
                nextIndex = _character.currentBox.Index + 1;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    Console.WriteLine($"Player {_character.PlayerIndex} walked right\n");
                    MoveDirection(battlefield, nextIndex);
                }
            }
            else if (direction == CharacterDirection.Up)
            {
                nextIndex = _character.currentBox.Index - battlefield.YLength;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    Console.WriteLine($"Player {_character.PlayerIndex} walked up\n");
                    MoveDirection(battlefield, nextIndex);
                }
            }
            else if (direction == CharacterDirection.Down)
            {
                nextIndex = _character.currentBox.Index + battlefield.YLength;
                if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                {
                    Console.WriteLine($"Player {_character.PlayerIndex} walked down\n");
                    MoveDirection(battlefield, nextIndex);
                }
            }
        }

        private CharacterDirection GetClosestDistanceComponent()
        {
            var target = _character.Target;
            var xDistance = Math.Abs(_character.currentBox.xIndex - _character.Target.currentBox.xIndex);
            var yDistance = Math.Abs(_character.currentBox.yIndex - _character.Target.currentBox.yIndex);

            if (xDistance == 0)
            {
                return _character.currentBox.yIndex > target.currentBox.yIndex ? CharacterDirection.Up : CharacterDirection.Down;
            }
            else if (yDistance == 0)
            {
                return _character.currentBox.xIndex > target.currentBox.xIndex ? CharacterDirection.Left : CharacterDirection.Right;
            }

            if (xDistance < yDistance)
            {
                // if the character x index is higher, it means that the character is positioned on the right of its target, so it must move left, and vice versa.
                return _character.currentBox.xIndex > target.currentBox.xIndex ? CharacterDirection.Left : CharacterDirection.Right;
            }
            else
            {
                // if the character y index is lower, it means that the character is positioned above its target, so it must move down, and vice versa.
                return _character.currentBox.yIndex > target.currentBox.yIndex ? CharacterDirection.Up : CharacterDirection.Down;
            }
        }

        // Moves the character for a new position and then redraw the grid
        private void MoveDirection(Grid battlefield, int nextIndex)
        {
            var nextBox = (battlefield.grids.Find(x => x.Index == nextIndex));
            var currentBox = _character.currentBox;
            if (nextBox.occupied)
            {
                return;
            }

            currentBox.occupied = false;
            battlefield.grids[currentBox.Index] = currentBox;
            _previouPosition = currentBox;

            nextBox.occupied = true;
            _character.currentBox = nextBox;
            battlefield.grids[nextBox.Index] = nextBox;

            GameManager.Instance.GridManager.DrawBattlefield();
        }

        // Checks in x and y directions if there is any character close enough to be a target.
        public bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == _character.currentBox.Index - 1).occupied);
            bool right = (battlefield.grids.Find(x => x.Index == _character.currentBox.Index + 1).occupied);
            bool up = (battlefield.grids.Find(x => x.Index == _character.currentBox.Index + battlefield.YLength).occupied);
            bool down = (battlefield.grids.Find(x => x.Index == _character.currentBox.Index - battlefield.YLength).occupied);

            if (left || right || up || down)
            {
                return true;
            }
            return false;
        }

        public void RewindPosition(Grid battlefield)
        {
            Console.WriteLine($"Player {_character.PlayerIndex} has been pushed away\n");
            var currentBox = _character.currentBox;
            var nextIndex = currentBox.Index + (_previouPosition.Index - currentBox.Index);
            MoveDirection(battlefield, nextIndex);
        }
    }
}