using cna.poo;
namespace cna {
    public partial class RegenerationVO : CardActionVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.Healing(1);
            ar.SelectSingleCard(acceptCallback_00, true);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            if (ar.SelectedUniqueCardId != 0) {
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.Healing(2);
            ar.SelectSingleCard(acceptCallback_01, true);
        }

        public void acceptCallback_01(ActionResultVO ar) {
            if (ar.SelectedUniqueCardId != 0) {
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
            }
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            } else if (UniqueId == card.UniqueId) {
                return "You can not select the unit being used!";
            } else if (ar.ActionIndex == 0 && card.UnitLevel > 2) {
                return "You can only ready a unit that is level 1 or 2!";
            } else if (ar.ActionIndex == 1 && card.UnitLevel > 3) {
                return "You can only ready a unit that is level 1, 2, or 3!";
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
    }
}
