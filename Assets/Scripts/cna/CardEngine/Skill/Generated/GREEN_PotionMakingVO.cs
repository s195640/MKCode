using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_PotionMakingVO : CardSkillVO {
        public GREEN_PotionMakingVO(int uniqueId) : base(
            uniqueId,
            "Potion Making",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKG_potion_making,
            Image_Enum.SKG_back,
            Image_Enum.A_MEEPLE_GREEN,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a round, except during combat, flip this for Heal 2"
            ) { }
    }
}
