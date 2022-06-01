using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_ShieldMasteryVO : CardSkillVO {
        public BLUE_ShieldMasteryVO(int uniqueId) : base(
            uniqueId,
            "Shield Mastery",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_shield_mastery,
            Image_Enum.SKB_back,
            Image_Enum.A_MEEPLE_BLUE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } },
            false,
            "Once a turn: Block 3, or Fire block 2, or Ice block 2"
            ) { }
    }
}
