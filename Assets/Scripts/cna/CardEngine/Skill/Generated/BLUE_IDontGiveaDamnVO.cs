using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_IDontGiveaDamnVO : CardSkillVO {
        public BLUE_IDontGiveaDamnVO(int uniqueId) : base(
            uniqueId,
            "I Don't Give a Damn",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_i_dont_give_a_damn,
            Image_Enum.SKB_back,
            Image_Enum.A_MEEPLE_BLUE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: One card played sideways gives you +2 instead of +1. If it's an advanced action, spell or artifact, it gives +3 instead."
            ) { }
    }
}
