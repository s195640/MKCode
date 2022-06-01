using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BannerofGloryVO : CardArtifactVO {
        public BannerofGloryVO(int uniqueId) : base(
            uniqueId,
            "Banner of Glory",
            Image_Enum.CT_banner_of_glory,
            new List<string> { "Assign this to a unit you control.  The assigned unit gets armor +1 and +1 to any attacks or blocks it makes. Whenever this unit attacks or blocks, fame +1", "Units you control get armor +1 and +1 to any attacks or blocks the make this turn.  Fame +1 for each unit that attacks or blocks this turn." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack } }
            ) { }
    }
}
