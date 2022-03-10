using AutoBattle.Enum;

namespace AutoBattle.Interfaces
{
    public interface IMoveAction
    {
        public void ExecuteAction(Grid battlefield);

        public bool CheckCloseTargets(Grid battlefield);

        public void RewindPosition(Grid battlefield);
    }
}