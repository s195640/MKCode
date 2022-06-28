using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_PowerofPainVO : CardSkillVO {
        public RED_PowerofPainVO(int uniqueId) : base(
            uniqueId,
            "Power of Pain",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_power_of_pain,
            Image_Enum.SKR_back,
            Image_Enum.A_meeple_arythea,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: You can play one Wound sideways, as if it were a non-Wound card. It gives +2 instead of +1. At the end of your turn, put that Wound in your discard pile."
            ) { }
    }
}
