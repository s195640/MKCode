using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SnowstormVO : CardSpellVO {
        public SnowstormVO(int uniqueId) : base(
            uniqueId,
            "Snowstorm",
            "Blizzard",
            CardType_Enum.Spell,
            Image_Enum.CS_snowstorm,
            new List<string> { "Ranged Ice Attack 5", "Take a Wound. Siege Ice Attack 8." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.Blue, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.Blue
            ) { }
    }
}
