using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BannerofProtectionVO : CardArtifactVO {
        public BannerofProtectionVO(int uniqueId) : base(
            uniqueId,
            "Banner of Protection",
            Image_Enum.CT_banner_of_protection,
            new List<string> { "Assign this to a unit you control. The assigned unit gets armor +1, fire resistance and ice resistance.", "At the end of your turn, you may throw away all wounds in your hand." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
