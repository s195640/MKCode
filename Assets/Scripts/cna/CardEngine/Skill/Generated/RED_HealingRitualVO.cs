using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_HealingRitualVO : CardSkillVO {
        public RED_HealingRitualVO(int uniqueId) : base(
            uniqueId,
            "Healing Ritual",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKR_healing_ritual,
            Image_Enum.SKR_back,
            Image_Enum.A_meeple_arythea,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            true,
            "Once a Round, except in combat: Flip this to throw away up to two Wounds from your hand. One of them goes to the hand of the closest hero (if tied, you choose) instead of to the Wound pile"
            ) { }
    }
}
