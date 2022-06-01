using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class PathFindingVO : CardActionVO {
        public PathFindingVO(int uniqueId) : base(
            uniqueId,
            "Path Finding",
            Image_Enum.CA_path_finding,
            CardType_Enum.Advanced,
            new List<string> { "Move 2. The Move cost of all terrains is reduced by 1, to a minimum of 2, this turn.", "Move 4. The Move cost of all terrains is reduced to 2 this turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { } },
            CardColor_Enum.Green
            ) { }
    }
}
