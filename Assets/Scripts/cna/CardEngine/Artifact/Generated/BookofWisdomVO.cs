using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BookofWisdomVO : CardArtifactVO {
        public BookofWisdomVO(int uniqueId) : base(
            uniqueId,
            "Book of Wisdom",
            Image_Enum.CT_book_of_wisdom,
            new List<string> { "Throw away an Action card from your hand. Gain an Advanced Action card from the Advanced Actions offer to your hand that is the same color as the Action you threw away.", "Throw away an Action card from your hand. Gain a Spell from the Spells offer to your hand that is the same color as the Action you threw away and a Crystal of that color to your Inventory." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
