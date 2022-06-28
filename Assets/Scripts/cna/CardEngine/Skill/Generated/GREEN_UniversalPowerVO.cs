using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_UniversalPowerVO : CardSkillVO {
        public GREEN_UniversalPowerVO(int uniqueId) : base(
            uniqueId,
            "Universal Power",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKG_universal_power,
            Image_Enum.SKG_back,
            Image_Enum.A_meeple_goldyx,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            false,
            "Once a turn:  You may add one mana to a card played sideways. If you do, the card gives +3 instead of +1. If it is an Action or Spell card of the same color as that mana, it gives +4."
            ) { }
    }
}
