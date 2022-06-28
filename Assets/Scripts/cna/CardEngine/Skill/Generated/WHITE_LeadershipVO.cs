using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_LeadershipVO : CardSkillVO {
        public WHITE_LeadershipVO(int uniqueId) : base(
            uniqueId,
            "Leadership",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKW_leadership,
            Image_Enum.SKW_back,
            Image_Enum.A_meeple_norowas,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: When activating a Unit, add +3 to its Block, or +2 to its Attack. or +1 to its Ranged (not Siege) Attack, regardless of its elements."
            ) { }
    }
}
