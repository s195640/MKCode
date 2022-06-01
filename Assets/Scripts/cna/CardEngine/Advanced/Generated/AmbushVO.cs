using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AmbushVO : CardActionVO {
        public AmbushVO(int uniqueId) : base(
            uniqueId,
            "Ambush",
            Image_Enum.CA_ambush,
            CardType_Enum.Advanced,
            new List<string> { "Move 2. Add +1 to your first Attack card of any type or +2 to your first Block card of any type, whichever you play first this turn.", "Move 4. Add +2 to your first Attack card of any type or +4 to your first Block card of any type, whichever you play first this turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Green } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack } },
            CardColor_Enum.Green
            ) { }
    }
}
