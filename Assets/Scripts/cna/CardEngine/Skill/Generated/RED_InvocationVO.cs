using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_InvocationVO : CardSkillVO {
        public RED_InvocationVO(int uniqueId) : base(
            uniqueId,
            "Invocation",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_invocation,
            Image_Enum.SKR_back,
            Image_Enum.A_meeple_arythea,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a turn: Discard a Wound card to gain a red or black mana token, or discard a non-wound card to gain a white or green mana token. You must spend a mana token gained this way immediately, or you cannot use the ability."
            ) { }
    }
}
