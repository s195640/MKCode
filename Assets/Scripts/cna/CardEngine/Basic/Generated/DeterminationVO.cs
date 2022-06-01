using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class DeterminationVO : CardActionVO {
        public DeterminationVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Determination",
            Image_Enum.CB_determination,
            CardType_Enum.Basic,
            new List<string> { "Attack 2 or Block 2", "Block 5" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } },
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
