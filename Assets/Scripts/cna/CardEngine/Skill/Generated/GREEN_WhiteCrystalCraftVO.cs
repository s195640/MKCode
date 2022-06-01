using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_WhiteCrystalCraftVO : CardSkillVO {
        public GREEN_WhiteCrystalCraftVO(int uniqueId) : base(
            uniqueId,
            "White Crystal Craft",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKG_white_crystal_craft,
            Image_Enum.SKG_back,
            Image_Enum.A_MEEPLE_GREEN,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a round, flip this to gain a Blue crystal to your inventory and a white mana token"
            ) { }
    }
}
