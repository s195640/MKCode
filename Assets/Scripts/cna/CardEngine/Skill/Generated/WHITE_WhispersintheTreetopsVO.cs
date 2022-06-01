using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WHITE_WhispersintheTreetopsVO : CardSkillVO {
        public WHITE_WhispersintheTreetopsVO(int uniqueId) : base(
            uniqueId,
            "Whispers in the Treetops",
            CardType_Enum.Skill,
            SkillRefresh_Enum.Round,
            Image_Enum.SKW_whisper_in_the_treetops,
            Image_Enum.SKW_back,
            Image_Enum.A_MEEPLE_WHITE,
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } },
            false,
            "Once a Round: Flip this to gain one white crystal to your Inventory, and one green mana token."
            ) { }
    }
}
