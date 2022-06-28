using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_ForwardMarchVO : CardSkillVO {
        public WHITE_ForwardMarchVO(int uniqueId) : base(
            uniqueId,
            "Forward March",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKW_forward_march,
            Image_Enum.SKW_back,
            Image_Enum.A_meeple_norowas,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn: Get Move 1 for each Ready and Unwounded Unit you control, to maximum of Move 3"
            ) { }
    }
}
