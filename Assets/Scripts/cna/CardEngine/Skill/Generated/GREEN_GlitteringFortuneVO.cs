using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_GlitteringFortuneVO : CardSkillVO {
        public GREEN_GlitteringFortuneVO(int uniqueId) : base(
            uniqueId,
            "Glittering Fortune",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKG_glittering_fortune,
            Image_Enum.SKG_back,
            Image_Enum.A_meeple_goldyx,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Influence } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn, during interaction: Get Influence 1 for each different color crystal in your Inventory. This Influence cannot be used outside of interaction."
            ) { }
    }
}
