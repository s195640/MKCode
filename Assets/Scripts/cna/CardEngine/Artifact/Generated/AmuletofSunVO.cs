using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AmuletofSunVO : CardArtifactVO {
        public AmuletofSunVO(int uniqueId) : base(
            uniqueId,
            "Amulet of Sun",
            Image_Enum.CT_amulet_of_sun,
            new List<string> { "Gain a gold mana token. If played during the Night, forests have their move cost reduced to 3, you can use gold mana, and you reveal garrisons of nearby fortified sites and all ruins as if it were day.", "Same as the basic effect, except you get three gold mana tokens instead of one." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
