using cna.poo;

namespace cna {
    public partial class RejuvenateVO : CardActionVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Healing", Image_Enum.I_healHand),
                new OptionVO("Draw Card", Image_Enum.I_cardBackRounded),
                new OptionVO("Green Mana", Image_Enum.I_mana_green),
                new OptionVO("Ready I or II", Image_Enum.I_healHand)
                );
        }

        public void acceptCallback_00(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.Healing(1);
                    ar.FinishCallback(ar);
                    break;
                }
                case 1: {
                    ar.DrawCard(1, ar.FinishCallback);
                    break;
                }
                case 2: {
                    ar.ManaGreen(1);
                    ar.FinishCallback(ar);
                    break;
                }
                case 3: {
                    ar.SelectSingleCard(acceptCallback_unit);
                    break;
                }
            }
        }

        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Healing", Image_Enum.I_healHand),
                new OptionVO("Draw Card", Image_Enum.I_cardBackRounded),
                new OptionVO("Green Crystal", Image_Enum.I_crystal_green),
                new OptionVO("Ready I,II,III", Image_Enum.I_healHand)
                );
        }

        public void acceptCallback_01(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.Healing(2);
                    ar.FinishCallback(ar);
                    break;
                }
                case 1: {
                    ar.DrawCard(2, ar.FinishCallback);
                    break;
                }
                case 2: {
                    ar.CrystalGreen(1);
                    ar.FinishCallback(ar);
                    break;
                }
                case 3: {
                    ar.SelectSingleCard(acceptCallback_unit);
                    break;
                }
            }
        }

        public void acceptCallback_unit(GameAPI ar) {
            if (ar.SelectedUniqueCardId != 0) {
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
            }
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
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
