using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class FireBoltVO : CardActionVO {
        public FireBoltVO(int uniqueId) : base(
            uniqueId,
            "Fire Bolt",
            Image_Enum.CA_fire_bolt,
            CardType_Enum.Advanced,
            new List<string> { "Gain a red crystal to your Inventory.","Ranged Fire Attack 3" },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Red}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Battle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack}},
            CardColor_Enum.Red
            ) { }
    }
}
