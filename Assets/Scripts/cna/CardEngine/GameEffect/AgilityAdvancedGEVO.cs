using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class AgilityAdvancedGEVO : CardGameEffectVO {
        public AgilityAdvancedGEVO(int uniqueId) : base(
            uniqueId, "Agility Advanced", Image_Enum.I_boots,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Agility02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During combat this turn, you may spend any amount of Move points: 1 to get Attack 1 and/or 2 to get Ranged Attack 1.",
                true, true, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } };
        }

        public override GameAPI ActionValid_00(GameAPI ar) {
            switch (ar.P.Battle.BattlePhase) {
                case BattlePhase_Enum.RangeSiege: {
                    if (ar.P.Movement > 1) {
                        ar.ActionMovement(-2);
                        ar.BattleRange(new AttackData(1));
                    } else {
                        ar.ErrorMsg = "You do not have enough movement points";
                    }
                    break;
                }
                case BattlePhase_Enum.Attack: {
                    if (ar.P.Movement > 0) {
                        ar.ActionMovement(-1);
                        ar.BattleAttack(new AttackData(1));
                    } else {
                        ar.ErrorMsg = "You do not have enough movement points";
                    }
                    break;
                }
            }
            return ar;
        }
    }
}
