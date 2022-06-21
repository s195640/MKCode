using System.Collections.Generic;
using cna.poo;


namespace cna {
    public class HornOfWrathGEVO : CardGameEffectVO {
        public HornOfWrathGEVO(int uniqueId) : base(
            uniqueId, "Horn of Wrath", Image_Enum.I_catapult,
                CardType_Enum.GameEffect,
                GameEffect_Enum.CT_HornOfWrath,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "Add +5 Siege Attack, +1 Wound.",
                false, true, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Attack } };
        }

        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.BattleSiege(new AttackData(5));
            ar.AddWound(1);
            return ar;
        }
    }
}
