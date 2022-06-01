using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RefreshingWalkVO : CardActionVO {
        public RefreshingWalkVO(int uniqueId) : base(
            uniqueId,
            "Refreshing Walk",
            Image_Enum.CA_refreshing_walk,
            CardType_Enum.Advanced,
            new List<string> { "Move 2 and Heal 1. If played during Combat, Move 2 only.", "Move 4 and Heal 2.  If played during Combat, Move 4 only." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { }
    }
}
