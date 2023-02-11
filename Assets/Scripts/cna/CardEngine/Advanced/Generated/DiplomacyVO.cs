using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class DiplomacyVO : CardActionVO {
        public DiplomacyVO(int uniqueId) : base(
            uniqueId,
            "Diplomacy",
            Image_Enum.CA_diplomacy,
            CardType_Enum.Advanced,
            new List<string> { "Influence 2. You may use Influence as Block this turn.","Influence 4. Choose Ice or Fire. You may use Influence as Block for the chosen element this turn." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.White}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block}},
            CardColor_Enum.White
            ) { }
    }
}
