using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class FireballVO : CardSpellVO {
        public FireballVO(int uniqueId) : base(
            uniqueId,
            "Fireball",
            "Firestorm",
            CardType_Enum.Spell,
            Image_Enum.CS_fireball,
            new List<string> { "Ranged Fire Attack 5.", "Take a Wound. Siege Fire Attack 8." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Red, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.Red
            ) { }
    }
}
