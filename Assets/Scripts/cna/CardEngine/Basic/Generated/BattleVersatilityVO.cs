using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BattleVersatilityVO : CardActionVO {
        public BattleVersatilityVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Battle Versatility",
            Image_Enum.CB_battle_versatility,
            CardType_Enum.Basic,
            new List<string> { "Attack 2, Block 2, or Range Attack 1.","Attack 4, Block 4, Fire Attack 3, Fire Block 3, Ranged Attack 3, or Seiged Attack 2." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.Attack}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.Attack}},
            CardColor_Enum.Red
            ) { Avatar = avatar; }
    }
}
