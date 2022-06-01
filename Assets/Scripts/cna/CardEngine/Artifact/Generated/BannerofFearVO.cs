using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BannerofFearVO : CardArtifactVO {
        public BannerofFearVO(int uniqueId) : base(
            uniqueId,
            "Banner of Fear",
            Image_Enum.CT_banner_of_fear,
            new List<string> { "Assign this to a unit you control. During the assign damage phase of combat, you may spend this Unit to cancel one enemy attack. If you do, Fame +1.", "Enemies do not attack this combat." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage } }
            ) { }
    }
}
