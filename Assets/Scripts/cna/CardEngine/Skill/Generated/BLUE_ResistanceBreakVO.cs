using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_ResistanceBreakVO : CardSkillVO {
        public BLUE_ResistanceBreakVO(int uniqueId) : base(
            uniqueId,
            "Resistance Break",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_resistance_break,
            Image_Enum.SKB_back,
            Image_Enum.A_MEEPLE_BLUE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Choose an enemy token It gets armor -1 for each resistance it has To a minimum of 1"
            ) { }
    }
}
