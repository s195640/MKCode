using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaBoltVO : CardSpellVO {
        public ManaBoltVO(int uniqueId) : base(
            uniqueId,
            "Mana Bolt",
            "Mana Thunderbolt",
            CardType_Enum.Spell,
            Image_Enum.CS_mana_bolt,
            new List<string> { "When you play this, pay a mana. If you paid blue, Ice Attack 8. If you paid red, Cold Fire Attack 7. If you paid white, Ranged Ice Attack 6. If you paid green, Siege Ice Attack 5.", "When you play this, pay a mana. If you paid blue, Ice Attack 11. If you paid red, Cold Fire Attack 10. If you paid white, Ranged Ice Attack 9. If you paid green, Siege Ice Attack 8." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Blue }, new List<Crystal_Enum>() { Crystal_Enum.Blue, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack }, new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } },
            CardColor_Enum.Blue
            ) { }
    }
}
