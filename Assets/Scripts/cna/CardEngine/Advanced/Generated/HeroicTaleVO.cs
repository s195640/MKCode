using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class HeroicTaleVO : CardActionVO {
        public HeroicTaleVO(int uniqueId) : base(
            uniqueId,
            "Heroic Tale",
            Image_Enum.CA_heroic_tale,
            CardType_Enum.Advanced,
            new List<string> { "Influence 3. Reputation +1 for each Unit you recruit this turn.","Influence 6. Fame +1 and Reputation +1 for each Unit you recruit this turn,." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.White}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.White
            ) { }
    }
}
