using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class WingsofNightGEVO : CardGameEffectVO {
        public WingsofNightGEVO(int uniqueId) : base(
            uniqueId, "Wings of Night", Image_Enum.I_shield,
            CardType_Enum.GameEffect,
            GameEffect_Enum.CS_WingsOfNight,
            GameEffectDuration_Enum.Turn,
            CNAColor.ColorLightBlue,
            "Target enemy does not attack this combat. Cost to activate this ability will increase by 1 movement point for each monster it is activated on starting at 0.",
            false, true, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.AssignDamage } };
        }

        public int totalMonstersBlocked = 0;

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.SelectedMonsters.Count == 1) {
                if (ar.LocalPlayer.Movement >= totalMonstersBlocked) {
                    int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
                    ar.LocalPlayer.Battle.Monsters[monsterId].Blocked = true;
                    ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack.");
                    ar.ActionMovement(-1 * totalMonstersBlocked);
                    totalMonstersBlocked++;
                } else {
                    ar.ErrorMsg = "You do not have enough movement points.  You need " + totalMonstersBlocked + " to use this ability!";
                }
            } else {
                ar.ErrorMsg = "You must select a monster to use this effect on!";
            }
            return ar;
        }
    }
}
