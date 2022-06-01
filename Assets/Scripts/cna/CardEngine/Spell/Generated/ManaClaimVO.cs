using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaClaimVO : CardSpellVO {
        public ManaClaimVO(int uniqueId) : base(
            uniqueId,
            "Mana Claim",
            "Mana Curse",
            CardType_Enum.Spell,
            Image_Enum.CS_mana_claim,
            new List<string> { "Take a mana die of a basic color from the Source and keep it in your Play area until the end of the Round. You can either gain 3 mana tokens of that color this turn, or one mana token of that color each turn for the remainder of the Round (starting with your next turn).", "Same as the basic effect. In addition, until the end of the Round: Each time another player uses one or more mana of that color on their turn (from any source), they take a Wound. Each player can get only one Wound per turn this way." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.Blue, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Blue
            ) { }
    }
}
