using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class DecomposeVO : CardActionVO {
        public DecomposeVO(int uniqueId) : base(
            uniqueId,
            "Decompose",
            Image_Enum.CA_decompose,
            CardType_Enum.Advanced,
            new List<string> { "When you play this card, throw away an Action card from hand. Gain two crystals to your Inventory that are the same color as the thrown away card.", "When you play this card, throw away an Action card from hand. Gain a crystal to your Inventory of each basic color that does not match the color of the thrown away card." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.NA }, new List<Crystal_Enum>() { Crystal_Enum.Red } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            CardColor_Enum.Red
            ) { }
    }
}
