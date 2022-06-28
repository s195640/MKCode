using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_GreenCrystalCraftVO : CardSkillVO {
        public GREEN_GreenCrystalCraftVO(int uniqueId) : base(
            uniqueId,
            "Green Crystal Craft",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKG_green_crystal_craft,
            Image_Enum.SKG_back,
            Image_Enum.A_meeple_goldyx,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a Round: Flip this to gain one blue crystal to your Inventory, and one green mana token."
            ) { }
    }
}
