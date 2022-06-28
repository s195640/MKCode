using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_IFeelNoPainVO : CardSkillVO {
        public BLUE_IFeelNoPainVO(int uniqueId) : base(
            uniqueId,
            "I Feel No Pain",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_i_feel_no_pain,
            Image_Enum.SKB_back,
            Image_Enum.A_meeple_tovak,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn, except in combat: Discard one wound from hand. if you do, draw a new card."
            ) { }
    }
}
