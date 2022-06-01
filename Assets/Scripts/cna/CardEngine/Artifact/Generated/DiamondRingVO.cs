using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class DiamondRingVO : CardArtifactVO {
        public DiamondRingVO(int uniqueId) : base(
            uniqueId,
            "Diamond Ring",
            Image_Enum.CT_diamond_ring,
            new List<string> { "Gain a white mana token and a white crystal to your Inventory. Fame +1", "You have an endless supply of white and black mana this turn. Fame +1 for each white Spell you cast this turn." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
