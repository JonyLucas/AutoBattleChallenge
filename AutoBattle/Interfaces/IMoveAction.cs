using AutoBattle.Enum;

namespace AutoBattle.Interfaces
{
    public interface IMoveAction
    {
        public void ExecuteAction(Character character, Grid battlefield);

        public bool CheckCloseTargets(Character character, Grid battlefield);
    }
}