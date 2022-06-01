using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaMeltdownVO : CardSpellVO {
        public ManaMeltdownVO(int uniqueId) : base(
            uniqueId,
            "Mana Meltdown",
            "Mana Radiance",
            CardType_Enum.Spell,
            Image_Enum.CS_mana_meltdown,
            new List<string> { "Each other player must randomly choose a crystal in their Inventory to be lost. You may gain one crystal lost this way to your Inventory. Any player that had no crystal in their Inventory when you played this, takes a Wound instead.", "When you play this, choose a basic mana color. Each player, including you, takes a Wound for each crystal of that color they own. Gain two crystal of the chosen color to your Inventory" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Red, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Red
            ) { }
    }
}
