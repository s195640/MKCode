using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RageVO : CardActionVO {
        public RageVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Rage",
            Image_Enum.CB_rage,
            CardType_Enum.Basic,
            new List<string> { "Attack 2 or Block 2","Attack 4" },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.Block, BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.Attack}},
            CardColor_Enum.Red
            ) { Avatar = avatar; }
    }
}
