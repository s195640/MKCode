using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BLUE_WhoNeedsMagicVO : CardSkillVO {
        public BLUE_WhoNeedsMagicVO(int uniqueId) : base(
            uniqueId,
            "Who Needs Magic",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKB_who_needs_magic,
            Image_Enum.SKB_back,
            Image_Enum.A_MEEPLE_BLUE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            false,
            "Once a turn: One card played sideways gives you +2 instead of +1. If you use no die from the source this turn, it gives +3 instead."
            ) { }
    }
}
