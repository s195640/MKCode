using cna.poo;

namespace cna {
    public partial class HerbalistsVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.Healing(2);
            return ar;
        }


        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_01);
        }

        public void acceptCallback_01(ActionResultVO ar) {
            ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            } else if (UniqueId == card.UniqueId) {
                return "You can not select the unit being used!";
            } else if (card.UnitLevel > 2) {
                return "You can only ready a unit that is level 1 or level 2!";
            } else {
                if (D.LocalPlayer.Deck.State.ContainsKey(card.UniqueId)) {
                    if (!D.LocalPlayer.Deck.State[card.UniqueId].ContainsAny(CardState_Enum.Unit_Exhausted)) {
                        return "The Unit must be Exhausted to be readied!";
                    }
                } else {
                    return "The Unit must be Exhausted to be readied!";
                }
            }
            return msg;
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.ManaGreen(1);
            return ar;
        }
    }
}
