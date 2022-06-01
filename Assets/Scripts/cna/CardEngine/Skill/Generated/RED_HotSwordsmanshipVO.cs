using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_HotSwordsmanshipVO : CardSkillVO {
        public RED_HotSwordsmanshipVO(int uniqueId) : base(
            uniqueId,
            "Hot Swordsmanship",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_hot_swordsmanship,
            Image_Enum.SKR_back,
            Image_Enum.A_MEEPLE_RED,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack } },
            false,
            "Once a turn: Attack 2, or Fire Attack 2."
            ) { }
    }
}
