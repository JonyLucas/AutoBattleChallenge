using AutoBattle.Enum;

namespace AutoBattle
{
    public class Types
    {
        public struct CharacterClassSpecific
        {
            private CharacterClass CharacterClass;
            private float hpModifier;
            private float ClassDamage;
            private CharacterSkills[] skills;
        }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool ocupied;
            public int Index;

            public GridBox(int x, int y, bool ocupied, int index)
            {
                xIndex = x;
                yIndex = y;
                this.ocupied = ocupied;
                this.Index = index;
            }
        }

        public struct CharacterSkills
        {
            private string Name;
            private float damage;
            private float damageMultiplier;
        }
    }
}