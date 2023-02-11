using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class IntimidateVO : CardActionVO {
        public IntimidateVO(int uniqueId) : base(
            uniqueId,
            "Intimidate",
            Image_Enum.CA_intimidate,
            CardType_Enum.Advanced,
            new List<string> { "Influence 4 or Attack 3. Reputation -1.","Influence 8 or Attack 7. Reputation -2." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence, TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.Attack}},
            CardColor_Enum.Red
            ) { }
    }
}
