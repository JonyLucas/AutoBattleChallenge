using AutoBattle.Enum;
using AutoBattle.Game;
using AutoBattle.Interfaces;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    /// <summary>
    /// Base Character class, with the basic logic for the character behaviour. Every Character class should extend from this class.
    /// </summary>
    public abstract class BaseCharacter
    {
        public GridBox currentBox;
        private readonly bool _isPlayer;
        private readonly GameManager _gameManager;

        #region HAS-A Relationship

        /*
         * Encapsulate what varies principle will allow to change these behaviours, with a HAS-A relationship.
         * So we can change the attack behaviour and the move behaviour in run time, as it needed.
         * Let's say we have a new movement behaviour, so our character can move vertically, horizontally and in diagonal directions.
         * All we need to do is to create a new IMoveAction class, with this especific logic,
         * and pass an instance of this class to the IMoveAction field.
         *
        **/

        protected IMoveAction moveAction;

        protected IAttackAction attackAction;

        #endregion HAS-A Relationship

        #region Properties

        public string Name { get; set; }
        public float Health { get; set; }
        public float BaseDamage { get; set; }
        public float DamageMultiplier { get; set; }
        public int PlayerIndex { get; set; }
        public Character Target { get; set; }
        public bool IsDead { get; set; }

        public IMoveAction MoveAction
        { get { return moveAction; } }

        public IAttackAction AttackAction
        { get { return attackAction; } }

        public bool IsPlayer
        { get { return _isPlayer; } }

        #endregion Properties

        protected BaseCharacter(CharacterClass characterClass, bool isPlayer = false)
        {
            IsDead = false;
            _isPlayer = isPlayer;
            _gameManager = GameManager.Instance;
        }

        public void StartTurn()
        {
            if (IsDead)
            {
                return;
            }

            var battlefield = _gameManager.GridManager.Grid;
            if (MoveAction.CheckCloseTargets(battlefield))
            {
                AttackAction.Attack();
            }
            else
            {
                MoveAction.ExecuteAction(battlefield);
            }
        }

        /// <summary>
        /// Changes the move behaviour of the character.
        /// PS: Not tested yet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetMoveAction<T>() where T : IMoveAction
        {
            var newMoveAction = typeof(T).GetConstructors().FirstOrDefault().Invoke(new[] { this });
            moveAction = (IMoveAction)newMoveAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetAttackAction<T>() where T : IAttackAction
        {
            var newAttackAction = typeof(T).GetConstructors().FirstOrDefault().Invoke(new[] { this });
            attackAction = (IAttackAction)newAttackAction;
        }
    }
}