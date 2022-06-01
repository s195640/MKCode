using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_DarkFireMagicVO : CardSkillVO {
        public RED_DarkFireMagicVO(int uniqueId) : base(
            uniqueId,
            "Dark Fire Magic",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKR_dark_fire_magic,
            Image_Enum.SKR_back,
            Image_Enum.A_MEEPLE_RED,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a Round: Flip this to gain one red crystal to your inventory, and one red or one black mana token."
            ) { }
    }
}
