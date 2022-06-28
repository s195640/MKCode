using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_InspirationVO : CardSkillVO {
        public WHITE_InspirationVO(int uniqueId) : base(
            uniqueId,
            "Inspiration",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKW_inspiration,
            Image_Enum.SKW_back,
            Image_Enum.A_meeple_norowas,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a Round, except in combat: Flip this to Ready or Heal a Unit"
            ) { }
    }
}
