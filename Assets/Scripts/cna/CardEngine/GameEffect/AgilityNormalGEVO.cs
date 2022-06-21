using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class AgilityNormalGEVO : CardGameEffectVO {
        public AgilityNormalGEVO(int uniqueId) : base(
            uniqueId, "Agility Normal", Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Agility01,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During combat this turn, you may spend Move points to get Attack 1 for each.",
                true, true, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Attack } };
        }

        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Movement > 0) {
                ar.ActionMovement(-1);
                ar.BattleAttack(new AttackData(1));
            } else {
                ar.ErrorMsg = "You do not have enough movement points";
            }
            return ar;
        }
    }
}
