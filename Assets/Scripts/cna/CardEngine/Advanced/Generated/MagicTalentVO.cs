using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class MagicTalentVO : CardActionVO {
        public MagicTalentVO(int uniqueId) : base(
            uniqueId,
            "Magic Talent",
            Image_Enum.CA_magic_talent,
            CardType_Enum.Advanced,
            new List<string> { "Discard a card of any color. You may play one Spell card of the same color in the Spells offer this turn as if it were in your hand. That card remains in the Spells offer.", "When you play this, pay a mana of any color. Gain a Spell card of that color from the Spells offer and put it in your discard pile." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Blue } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Blue
            ) { }
    }
}
