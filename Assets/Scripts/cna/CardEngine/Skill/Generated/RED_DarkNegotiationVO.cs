using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_DarkNegotiationVO : CardSkillVO {
        public RED_DarkNegotiationVO(int uniqueId) : base(
            uniqueId,
            "Dark Negotiation",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_dark_negotiation,
            Image_Enum.SKR_back,
            Image_Enum.A_meeple_arythea,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn: Influence 2 (during the Day), or Influence 3 (at Night)."
            ) { }
    }
}
