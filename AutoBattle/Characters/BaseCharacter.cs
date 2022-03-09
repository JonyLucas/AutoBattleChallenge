using AutoBattle.Enum;
using AutoBattle.Game;
using AutoBattle.Interfaces;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    public abstract class BaseCharacter
    {
        public GridBox currentBox;
        private readonly bool _isPlayer;
        private readonly GameManager _gameManager;
        protected IMoveAction moveAction;
        protected IAttackAction attackAction;

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
    }
}