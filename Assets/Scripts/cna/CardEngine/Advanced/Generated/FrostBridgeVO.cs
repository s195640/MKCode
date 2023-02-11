using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class FrostBridgeVO : CardActionVO {
        public FrostBridgeVO(int uniqueId) : base(
            uniqueId,
            "Frost Bridge",
            Image_Enum.CA_frost_bridge,
            CardType_Enum.Advanced,
            new List<string> { "Move 2. The Move cost of swamps is reduced to 1 this turn.","Move 4. You are able to travel through lakes, and the Move cost of lakes and swamps is reduced to 1 this turn." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Blue}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Blue
            ) { }
    }
}
