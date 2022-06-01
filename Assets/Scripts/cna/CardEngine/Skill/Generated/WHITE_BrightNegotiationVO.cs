using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_BrightNegotiationVO : CardSkillVO {
        public WHITE_BrightNegotiationVO(int uniqueId) : base(
            uniqueId,
            "Bright Negotiation",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKW_bright_negotiation,
            Image_Enum.SKW_back,
            Image_Enum.A_MEEPLE_WHITE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn: Influence 3 (during the Day) or Influence 2 (at Night)"
            ) { }
    }
}
