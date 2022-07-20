using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class TirelessnessVO : CardActionVO {
        public TirelessnessVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Tirelessness",
            Image_Enum.CB_tirelessness,
            CardType_Enum.Basic,
            new List<string> { "Move 2. The next card providing Move (incl. card played sideways) gives 1 extra Move this turn.","Move 4. Each other card providing Move (incl. cards played sideways) gives 1 extra Move this turn." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Blue}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
