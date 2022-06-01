using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class SongofWindGEVO : CardGameEffectVO {
        public SongofWindGEVO(int uniqueId) : base(
            uniqueId, "Song of Wind Advanced", Image_Enum.I_boots,
            CardType_Enum.GameEffect,
            GameEffect_Enum.AC_SongOfWind02,
            GameEffectDuration_Enum.Turn,
            CNAColor.ColorLightBlue,
            "The Move cost of plains, deserts and wastelands is reduced by 2, to a minimum of 0. You may pay a blue mana to be able to travel through lakes for Move cost 0 this turn.",
            true, false, true
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { } };
        }

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (!ar.LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.AC_SongOfWind03)) {
                ar.AddGameEffect(GameEffect_Enum.AC_SongOfWind03);
            } else {
                ar.ErrorMsg = "You already have the Song of Winds travel through lakes ability.";
            }
            return ar;
        }
    }
}
