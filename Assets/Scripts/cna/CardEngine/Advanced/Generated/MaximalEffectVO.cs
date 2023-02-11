using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class MaximalEffectVO : CardActionVO {
        public MaximalEffectVO(int uniqueId) : base(
            uniqueId,
            "Maximal Effect",
            Image_Enum.CA_maximal_effect,
            CardType_Enum.Advanced,
            new List<string> { "When you play this, play another Action card with it. Use the basic effect of that card three times. Then, throw away that card.","When you play this, play another Action card with it. Use the advanced effect of that card two times (for free). Then, throw away that card." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}},
            CardColor_Enum.Red
            ) { }
    }
}
