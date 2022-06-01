using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SwiftnessVO : CardActionVO {
        public SwiftnessVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Swiftness",
            Image_Enum.CB_swiftness,
            CardType_Enum.Basic,
            new List<string> { "Move 2", "Ranged Attack 3" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.White } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.White
            ) { Avatar = avatar; }
    }
}
