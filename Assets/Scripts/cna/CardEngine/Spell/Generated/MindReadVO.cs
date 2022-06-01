using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class MindReadVO : CardSpellVO {
        public MindReadVO(int uniqueId) : base(
            uniqueId,
            "Mind Read",
            "Mind Steal",
            CardType_Enum.Spell,
            Image_Enum.CS_mind_read,
            new List<string> { "Choose a color. Gain a crystal of the chosen color to your Inventory. Each other player must discard a Spell or Action card of that color from their hand, or reveal their hand to show that they have none.", "Same as the basic effect. In addition to that , you may decide to permanently steal one of the Action cards (not Spells) discarded this way and put it into your hand." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.White }, new List<Crystal_Enum>() { Crystal_Enum.White, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.White
            ) { }
    }
}
