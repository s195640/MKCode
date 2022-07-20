using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RuthlessCoercionVO : CardActionVO {
        public RuthlessCoercionVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Ruthless Coercion",
            Image_Enum.CB_ruthless_coercion,
            CardType_Enum.Basic,
            new List<string> { "Influence 2. You may get a discount of 2 towards the cost of recruiting one Unit. If you recruit that Unit this turn then Reputation -1.","Influence 6. Reputation -1. You may ready level I and II Units you control by paying 2 Influence per level of Unit." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Influence}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {}, new List<BattlePhase_Enum>() {}},
            CardColor_Enum.Red
            ) { Avatar = avatar; }
    }
}
