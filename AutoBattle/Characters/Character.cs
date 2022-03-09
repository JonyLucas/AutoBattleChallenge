using AutoBattle.Characters.Actions;
using AutoBattle.Enum;

namespace AutoBattle.Characters
{
    public class Character : BaseCharacter
    {
        public Character(CharacterClass characterClass, bool isPlayer = false)
            : base(characterClass, isPlayer)
        {
            moveAction = new SimpleMoveAction(this);
            attackAction = new SimpleAttackAction(this);
        }
    }
}