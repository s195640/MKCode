using cna.poo;

namespace cna {
    public partial class ConcentrationVO : CardActionVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("Red Mana", Image_Enum.I_mana_red),
                new OptionVO("White Mana", Image_Enum.I_mana_white)
                );
        }

        public void acceptCallback_00(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.ManaBlue(1);
                    break;
                }
                case 1: {
                    ar.ManaRed(1);
                    break;
                }
                case 2: {
                    ar.ManaWhite(1);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }



        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_01);
        }

        public void acceptCallback_01(GameAPI ar) {
            ar.CardModifier = 2;
            CardActionVO selectedCard = (CardActionVO)D.Cards[ar.SelectedUniqueCardId];
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Advanced);
            selectedCard.ActionPaymentComplete_01(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = base.IsSelectionAllowed(card, cardHolder, ar);
            if (msg.Length == 0) {
                if (card.CardType == CardType_Enum.Basic || card.CardType == CardType_Enum.Advanced) {
                    GameAPI ar2 = new GameAPI(card.UniqueId, CardState_Enum.NA, 1);
                    checkAllowedToUse(ar2);
                    if (!ar2.Status) {
                        return ar2.ErrorMsg;
                    }
                } else {
                    msg = "You can only play Action cards!";
                }
            }
            return msg;
        }
    }
}
