namespace AutoBattle.Interfaces
{
    public interface IAttackAction
    {
        public void TakeDamage(float amount);

        public void Attack();
    }
}