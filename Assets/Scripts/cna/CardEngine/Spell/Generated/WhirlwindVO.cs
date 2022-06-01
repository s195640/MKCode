using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class WhirlwindVO : CardSpellVO {
        public WhirlwindVO(int uniqueId) : base(
            uniqueId,
            "Whirlwind",
            "Tornado",
            CardType_Enum.Spell,
            Image_Enum.CS_whirlwind,
            new List<string> { "Target enemy does not attack this combat.", "Play this only in the Attack phase of combat. Destroy target enemy." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.White }, new List<Crystal_Enum>() { Crystal_Enum.White, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack } },
            CardColor_Enum.White
            ) { }
    }
}
