using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RubyRingVO : CardArtifactVO {
        public RubyRingVO(int uniqueId) : base(
            uniqueId,
            "Ruby Ring",
            Image_Enum.CT_ruby_ring,
            new List<string> { "Gain a red mana token and a red crystal to your Inventory. Fame +1", "upply of red and black mana this turn. Fame +1 for each red Spell you cast this turn." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
