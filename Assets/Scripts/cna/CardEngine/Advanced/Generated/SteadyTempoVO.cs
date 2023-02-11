using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SteadyTempoVO : CardActionVO {
        public SteadyTempoVO(int uniqueId) : base(
            uniqueId,
            "Steady Tempo",
            Image_Enum.CA_steady_tempo,
            CardType_Enum.Advanced,
            new List<string> { "Move 2. At the end of your turn, instead of putting this card in your discard pile, you may place it on the bottom of your deed deck as long as it is not empty.","Move 4. At the end of your turn, instead of putting this card in your discard pile, you may place it on top of your Deed deck." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Blue}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Blue
            ) { }
    }
}
