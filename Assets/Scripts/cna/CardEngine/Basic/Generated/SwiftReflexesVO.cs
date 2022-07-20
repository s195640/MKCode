using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SwiftReflexesVO : CardActionVO {
        public SwiftReflexesVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Swift Reflexes",
            Image_Enum.CB_swift_reflexes,
            CardType_Enum.Basic,
            new List<string> { "Move 2, Ranged Attack 1, or reduce one enemy attack by 1.","Move 4, Ranged Attack 3, or reduce one enemy attack by 2." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.White}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack}},
            CardColor_Enum.White
            ) { Avatar = avatar; }
    }
}
