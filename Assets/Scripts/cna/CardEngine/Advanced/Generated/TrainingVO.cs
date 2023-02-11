using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class TrainingVO : CardActionVO {
        public TrainingVO(int uniqueId) : base(
            uniqueId,
            "Training",
            Image_Enum.CA_training,
            CardType_Enum.Advanced,
            new List<string> { "Throw away an Action card from your hand, then take a card of the same color from the Advanced Actions offer and put it into your discard pile.","Throw away an Action card from your hand, then take a card of the same color from the Advanced Actions offer and put it into your hand." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Green}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}},
            CardColor_Enum.Green
            ) { }
    }
}
