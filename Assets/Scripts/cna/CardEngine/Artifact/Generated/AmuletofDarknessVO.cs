using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AmuletofDarknessVO : CardArtifactVO {
        public AmuletofDarknessVO(int uniqueId) : base(
            uniqueId,
            "Amulet of Darkness",
            Image_Enum.CT_amulet_of_darkness,
            new List<string> { "Gain a mana token of any color. If played during the Day, deserts have their move cost reduced to 3 and you can use black mana as if it were Night.", "Same as the basic effect, except you get three mana tokens of any colors instead of one." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
