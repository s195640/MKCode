using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class SpaceBendingVO : CardSpellVO {
        public SpaceBendingVO(int uniqueId) : base(
            uniqueId,
            "Space Bending",
            "Time Bending",
            CardType_Enum.Spell,
            Image_Enum.CS_space_bending,
            new List<string> { "This turn, you may move to spaces that are 2 spaces away from you as if they were adjacent. Ignore any spaces you leap over this way. Your movement does not provoke rampaging enemies this turn.", "At the end of your turn, set this card aside for the rest of the Round. Put all other cards you played this turn (not those discarded or thrown away) back in your hand. Skip the draw new cards portion of your end of turn step. Immediately take another turn." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.Blue, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Blue
            ) { }
    }
}
