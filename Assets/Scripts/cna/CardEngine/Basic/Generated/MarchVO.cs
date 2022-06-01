using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class MarchVO : CardActionVO {
        public MarchVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "March",
            Image_Enum.CB_march,
            CardType_Enum.Basic,
            new List<string> { "Move 2", "Move 4" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { Avatar = avatar; }
    }
}
