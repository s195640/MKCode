using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class CrystalJoyVO : CardActionVO {
        public CrystalJoyVO(int uniqueId, Image_Enum avatar) : base(
            uniqueId,
            "Crystal Joy",
            Image_Enum.CB_crystal_joy,
            CardType_Enum.Basic,
            new List<string> { "Pay a mana of any basic color to gain a crystal of that color to your inventory. At the end of your turn, you may discard another non-Wound card to take this card back to your hand.","Gain a crystal of any basic color to your Inventory. At the end of your turn, you may discard another card including a Wound to take this card back to your hand." },
            new List<List<Crystal_Enum>> {new List<Crystal_Enum>() {Crystal_Enum.NA}, new List<Crystal_Enum>() {Crystal_Enum.Blue}},
            new List<List<TurnPhase_Enum>> {new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}, new List<TurnPhase_Enum>() {TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle}},
            new List<List<BattlePhase_Enum>> {new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}, new List<BattlePhase_Enum>() {BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle}},
            CardColor_Enum.Blue
            ) { Avatar = avatar; }
    }
}
