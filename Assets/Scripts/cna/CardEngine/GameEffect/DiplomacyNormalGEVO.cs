using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class DiplomacyNormalGEVO : CardGameEffectVO {
        public DiplomacyNormalGEVO(int uniqueId) : base(
            uniqueId, "Diplomacy Normal", Image_Enum.I_influencegrey,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Diplomacy01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During combat this turn, you may spend Influence points to get Block 1 for each.",
                true, true, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } };
        }

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Influence > 0) {
                ar.ActionInfluence(-1);
                ar.BattleBlock(new AttackData(1));
            } else {
                ar.ErrorMsg = "You do not have enough influence points";
            }
            return ar;
        }
    }
}
