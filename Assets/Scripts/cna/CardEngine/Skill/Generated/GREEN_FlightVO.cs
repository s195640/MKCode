using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_FlightVO : CardSkillVO {
        public GREEN_FlightVO(int uniqueId) : base(
            uniqueId,
            "Flight",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKG_flight,
            Image_Enum.SKG_back,
            Image_Enum.A_MEEPLE_GREEN,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { } },
            false,
            "Once a turn: Flip this to move to an adjacent space for free, or to move two spaces for 2 Move points. You must end this move in a safe space. This move does not provoke rampaging enemies."
            ) { }
    }
}
