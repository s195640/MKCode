using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_BondsofLoyaltyVO : CardSkillVO {
        public WHITE_BondsofLoyaltyVO(int uniqueId) : base(
            uniqueId,
            "Bonds of Loyalty",
            CardType_Enum.Skill,
            SkillRefresh_Enum.NA,
            Image_Enum.SKW_bonds_of_loyalty,
            Image_Enum.SKW_back,
            Image_Enum.A_MEEPLE_WHITE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "When you gain this Skill, add two Regular Units to the Units offer. Put this Skill in you Unit area as a Command token. It costs 5 less Influence (minimum 0) to recruit a Unit under this Command token. This Unit can be used even when the use of Units would normally not be allowed. You cannot disband this Unit."
            ) { }
    }
}
