using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ColdToughnessVO : CardActionVO {
        public ColdToughnessVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Cold Toughness",
            Image_Enum.CB_cold_toughness,
            CardType_Enum.Basic,
            new List<string> { "Ice Attack 2 or Ice Block 3.", "Ice Block 5. Get +1 Ice Block for each special ability." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } },
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
