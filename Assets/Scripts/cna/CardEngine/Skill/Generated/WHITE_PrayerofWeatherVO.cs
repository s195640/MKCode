using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_PrayerofWeatherVO : CardSkillVO {
        public WHITE_PrayerofWeatherVO(int uniqueId) : base(
            uniqueId,
            "Prayer of Weather",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKW_prayer_of_weather,
            Image_Enum.SKW_back,
            Image_Enum.A_MEEPLE_WHITE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            true,
            "Once a Round: Put this Skill token in the center. Until start of your next turn, the Move cost of all terrain is reduced by 2 for you (to a minimum of 1), and is increased by 1 for all other players (to a maximum of 5). Then take this token back and flip it."
            ) { }
    }
}
