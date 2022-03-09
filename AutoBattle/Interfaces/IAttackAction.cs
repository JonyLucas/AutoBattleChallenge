namespace AutoBattle.Interfaces
{
    public interface IAttackAction
    {
        public bool TakeDamage(float amount);

        public void Attack();
    }
}