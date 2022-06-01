using System.Collections.Generic;
using cna.poo;

namespace cna {
    class MagicGladeGEVO : CardGameEffectVO {
        public MagicGladeGEVO(int uniqueId) : base(
            uniqueId, "Magic Glade", Image_Enum.SH_MagicGlade,
                CardType_Enum.GameEffect,
                GameEffect_Enum.SH_MagicGlade,
                GameEffectDuration_Enum.Game,
                CNAColor.ColorLightBlue,
                "Day +1 Gold Mana at start of turn.\nNight +1 Black Mana at start of turn.\nEnd of turn trash 1 wound from hand or discard.",
                true, false, false
            ) {
            Actions = new List<string>() { "Trash 1 wound from hand or discard." };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.NA } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.NA } };
        }
    }
}