using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ThreatenVO : CardActionVO {
        public ThreatenVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Threaten",
            Image_Enum.CB_threaten,
            CardType_Enum.Basic,
            new List<string> { "Influence 2","Influence 5, Reputation -1" },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Red
            ) { Avatar = avatar; }
    }
}
