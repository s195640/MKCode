using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BurningShieldVO : CardSpellVO {
        public BurningShieldVO(int uniqueId) : base(
            uniqueId,
            "Burning Shield",
            "Exploding Shield",
            CardType_Enum.Spell,
            Image_Enum.CS_burning_shield,
            new List<string> { "Fire Block 4. If this card is used as part of a successful Block, you may use it during your Attack phase as Fire Attack 4.", "Fire Block 4. If this card is used as part of a successful Block, destroy the blocked enemy." },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum>() { Crystal_Enum.Red }, new List<Crystal_Enum>() { Crystal_Enum.Red, Crystal_Enum.Black } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle }, new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block }, new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } },
            CardColor_Enum.Red
            ) { }
    }
}
