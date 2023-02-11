using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RefreshingWalkVO : CardActionVO {
        public RefreshingWalkVO(int uniqueId) : base(
            uniqueId,
            "Refreshing Walk",
            Image_Enum.CA_refreshing_walk,
            CardType_Enum.Advanced,
            new List<string> { "Move 2 and Heal 1. If played during Combat, Move 2 only.","Move 4 and Heal 2.  If played during Combat, Move 4 only." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Green}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}},
            CardColor_Enum.Green
            ) { }
    }
}
