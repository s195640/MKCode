using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BannerofCourageVO : CardArtifactVO {
        public BannerofCourageVO(int uniqueId) : base(
            uniqueId,
            "Banner of Courage",
            Image_Enum.CT_banner_of_courage,
            new List<string> { "Assign this to a unit you control. Once a round, except during combat, you may flip this card face down to ready this unit. At the beginning of a round, flip it face up again.", "You may play this any time other than combat to ready all units you control." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { } }
            ) { }
    }
}
