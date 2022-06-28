using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_DoubleTimeVO : CardSkillVO {
        public BLUE_DoubleTimeVO(int uniqueId) : base(
            uniqueId,
            "Double Time",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_double_time,
            Image_Enum.SKB_back,
            Image_Enum.A_meeple_tovak,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn: Move 2 (during the Day), or Move 1 (during the Night)"
            ) { }
    }
}
