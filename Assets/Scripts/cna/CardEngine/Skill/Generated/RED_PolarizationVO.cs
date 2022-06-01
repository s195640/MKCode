using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_PolarizationVO : CardSkillVO {
        public RED_PolarizationVO(int uniqueId) : base(
            uniqueId,
            "Polarization",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Turn,
            Image_Enum.SKR_polarization,
            Image_Enum.SKR_back,
            Image_Enum.A_MEEPLE_RED,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a turn: You can use one mana as a mana of the opposite color (see token diagram). During the Day, you could use a black mana as any color other than black (not to power the stronger effect of spells). At Night, you could use a gold mana as black to power the stronger effect of a Spell, but not as any other color."
            ) { }
    }
}
