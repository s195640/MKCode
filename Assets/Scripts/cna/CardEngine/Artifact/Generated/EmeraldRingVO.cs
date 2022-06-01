using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class EmeraldRingVO : CardArtifactVO {
        public EmeraldRingVO(int uniqueId) : base(
            uniqueId,
            "Emerald Ring",
            Image_Enum.CT_emerald_ring,
            new List<string> { "Gain a green mana token and a green crystal to your Inventory. Fame +1", "You have an endless supply of green and black mana this turn. Fame +1 for each green Spell you cast this turn." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
