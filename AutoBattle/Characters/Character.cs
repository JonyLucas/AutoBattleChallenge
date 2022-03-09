using AutoBattle.Enum;
using AutoBattle.Game;
using System;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public GridBox currentBox;
        private readonly bool _isPlayer;
        private bool _isDead;
        private readonly GameManager _gameManager;

        public string Name { get; set; }
        public float Health { get; set; }
        public float BaseDamage { get; set; }
        public float DamageMultiplier { get; set; }
        public int PlayerIndex { get; set; }
        public Character Target { get; set; }

        public bool IsPlayer
        { get { return _isPlayer; } }

        public bool IsDead
        { get { return _isDead; } }

        public Character(CharacterClass characterClass, bool isPlayer = false)
        {
            _isPlayer = isPlayer;
            _isDead = false;
            _gameManager = GameManager.Instance;
        }

        public bool TakeDamage(float amount)
        {
            if ((Health -= amount) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            _isDead = true;
            var battlefield = _gameManager.GridManager.Grid;
            currentBox.occupied = false;
            battlefield.grids[currentBox.Index] = currentBox;
            _gameManager.GridManager.DrawBattlefield();
        }

        public void StartTurn()
        {
            if (_isDead)
            {
                return;
            }

            var battlefield = _gameManager.GridManager.Grid;
            if (CheckCloseTargets(battlefield))
            {
                Attack(Target);
            }
            else
            {
                var direction = GetClosestDistanceComponent();
                var nextIndex = -1;

                // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (direction == CharacterDirection.Left)
                {
                    nextIndex = currentBox.Index - 1;
                    if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                    {
                        MoveDirection(battlefield, nextIndex);
                        Console.WriteLine($"Player {PlayerIndex} walked left\n");
                    }
                }
                else if (direction == CharacterDirection.Right)
                {
                    nextIndex = currentBox.Index + 1;
                    if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                    {
                        MoveDirection(battlefield, nextIndex);
                        Console.WriteLine($"Player {PlayerIndex} walked right\n");
                    }
                }
                else if (direction == CharacterDirection.Up)
                {
                    nextIndex = currentBox.Index - battlefield.YLength;
                    if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                    {
                        MoveDirection(battlefield, nextIndex);
                        Console.WriteLine($"Player {PlayerIndex} walked up\n");
                    }
                }
                else if (direction == CharacterDirection.Down)
                {
                    nextIndex = currentBox.Index + battlefield.YLength;
                    if ((battlefield.grids.Exists(x => x.Index == nextIndex)))
                    {
                        MoveDirection(battlefield, nextIndex);
                        Console.WriteLine($"Player {PlayerIndex} walked down\n");
                    }
                }
            }
        }

        // If there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
        private CharacterDirection GetClosestDistanceComponent()
        {
            var xDistance = Math.Abs(currentBox.xIndex - Target.currentBox.xIndex);
            var yDistance = Math.Abs(currentBox.yIndex - Target.currentBox.yIndex);

            if (xDistance == 0)
            {
                return this.currentBox.yIndex > Target.currentBox.yIndex ? CharacterDirection.Up : CharacterDirection.Down;
            }
            else if (yDistance == 0)
            {
                return this.currentBox.xIndex > Target.currentBox.xIndex ? CharacterDirection.Left : CharacterDirection.Right;
            }

            if (xDistance < yDistance)
            {
                // if the character x index is higher, it means that the character is positioned on the right of its target, so it must move left, and vice versa.
                return this.currentBox.xIndex > Target.currentBox.xIndex ? CharacterDirection.Left : CharacterDirection.Right;
            }
            else
            {
                // if the character y index is lower, it means that the character is positioned above its target, so it must move down, and vice versa.
                return this.currentBox.yIndex > Target.currentBox.yIndex ? CharacterDirection.Up : CharacterDirection.Down;
            }
        }

        private void MoveDirection(Grid battlefield, int nextIndex)
        {
            currentBox.occupied = false;
            battlefield.grids[currentBox.Index] = currentBox;

            currentBox = (battlefield.grids.Find(x => x.Index == nextIndex));
            currentBox.occupied = true;
            battlefield.grids[currentBox.Index] = currentBox;

            _gameManager.GridManager.DrawBattlefield();
        }

        // Checks in x and y directions if there is any character close enough to be a target.
        private bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).occupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).occupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.YLength).occupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.YLength).occupied);

            if (left || right || up || down)
            {
                return true;
            }
            return false;
        }

        public void Attack(Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)BaseDamage));
            Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
        }
    }
}