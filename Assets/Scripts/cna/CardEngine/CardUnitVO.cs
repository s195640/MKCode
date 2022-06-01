using System;
using System.Collections.Generic;
using cna.poo;

namespace cna {
    [Serializable]
    public abstract class CardUnitVO : CardActionVO {

        public CardUnitVO(int uniqueId, string cardTitle, Image_Enum cardImage, CardType_Enum cardType, List<string> actions, List<List<Crystal_Enum>> costs, List<List<TurnPhase_Enum>> allowed, List<List<BattlePhase_Enum>> battleAllowed = null, CardColor_Enum cardColor = CardColor_Enum.NA, int cost = 0, int level = 0, int armor = 0, List<Image_Enum> recruitLocation = null, List<Image_Enum> resistance = null)
            : base(uniqueId, cardTitle, cardImage, cardType, actions, costs, allowed, battleAllowed, cardColor) {
            UnitCost = cost;
            UnitLevel = level;
            UnitArmor = armor;
            UnitRecruitLocation = recruitLocation;
            UnitResistance = resistance;
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            bool bondsOfLoyalty = false;
            if (ar.LocalPlayer.Deck.State.ContainsKey(ar.UniqueCardId)) {
                bondsOfLoyalty = ar.LocalPlayer.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_BondsOfLoyalty);
            }
            bool noUnits = ar.LocalPlayer.GameEffects.ContainsKeyAny(GameEffect_Enum.SH_Monastery, GameEffect_Enum.SH_Dungeon, GameEffect_Enum.SH_Tomb);
            if (!bondsOfLoyalty && ar.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.Battle && noUnits) {
                ar.ErrorMsg = "You can not use Units in this battle!";
                return ar;
            } else {
                return base.checkAllowedToUse(ar);
            }
        }


        public override string ToString() {
            string v = string.Format("{0}, cost = {1}, level = {2}, armor = {3}, recruitLocation = [{4}], resistance = [{5}]", base.ToString(), UnitCost, UnitLevel, UnitArmor, string.Join(": ", UnitRecruitLocation.ToArray()), string.Join(": ", UnitResistance.ToArray()));
            return v;
        }
    }
}
