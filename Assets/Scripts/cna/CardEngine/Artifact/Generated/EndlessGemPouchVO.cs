using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class EndlessGemPouchVO : CardArtifactVO {
        public EndlessGemPouchVO(int uniqueId) : base(
            uniqueId,
            "Endless Gem Pouch",
            Image_Enum.CT_endless_gem_pouch,
            new List<string> { "Gain two random crystals of basic type.", "Gain a mana token of each basic color. Also get one gold (if played at day) or black (if played at night) mana token." },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } }
            ) { }
    }
}
