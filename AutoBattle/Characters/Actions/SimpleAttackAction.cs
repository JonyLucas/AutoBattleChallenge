using AutoBattle.Game;
using AutoBattle.Interfaces;
using System;

namespace AutoBattle.Characters.Actions
{
    public class SimpleAttackAction : IAttackAction
    {
        private readonly Character _character;
        private readonly GameManager _gameManager;

        public SimpleAttackAction(Character character)
        {
            _character = character;
            _gameManager = GameManager.Instance;
        }

        public void TakeDamage(float amount)
        {
            if ((_character.Health -= amount) <= 0)
            {
                Die();
                return;
            }

            Console.WriteLine($"Player {_character.PlayerIndex} helth: {_character.Health}\n");
            PushAwayDamage();
        }

        private void Die()
        {
            _character.IsDead = true;
            var battlefield = _gameManager.GridManager.Grid;
            var currentBox = _character.currentBox;

            currentBox.occupied = false;
            _character.currentBox = currentBox;

            battlefield.grids[currentBox.Index] = currentBox;
            _gameManager.GridManager.DrawBattlefield();
        }

        public void Attack()
        {
            var target = _character.Target;
            var rand = new Random();
            var damage = rand.Next(0, (int)_character.BaseDamage);

            Console.WriteLine($"Player {_character.PlayerIndex} is attacking the player {target.PlayerIndex} and did {damage} damage\n");
            target.AttackAction.TakeDamage(damage);
        }

        /// <summary>
        /// The attack has a chance to push a character away (random chance)
        /// Implementation of the special feature related to the candidature.
        /// </summary>
        private void PushAwayDamage()
        {
            Random rand = new Random();
            var condition = rand.Next(0, 2);
            if (condition == 1)
            {
                _character.MoveAction.RewindPosition(_gameManager.GridManager.Grid);
            }
        }
    }
}