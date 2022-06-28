using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_DarkPathsVO : CardSkillVO {
        public RED_DarkPathsVO(int uniqueId) : base(
            uniqueId,
            "Dark Paths",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_dark_paths,
            Image_Enum.SKR_back,
            Image_Enum.A_meeple_arythea,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn: Move 1 (during the Day), or Move 2 (at Night)."
            ) { }
    }
}
