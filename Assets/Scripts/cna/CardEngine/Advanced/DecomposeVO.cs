using cna.poo;
namespace cna {
    public partial class DecomposeVO : CardActionVO {

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Trashed);
            switch (D.Cards[ar.SelectedUniqueCardId].CardColor) {
                case CardColor_Enum.Blue: { ar.CrystalBlue(2); break; }
                case CardColor_Enum.Green: { ar.CrystalGreen(2); break; }
                case CardColor_Enum.White: { ar.CrystalWhite(2); break; }
                case CardColor_Enum.Red: { ar.CrystalRed(2); break; }
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_01);
        }

        public void acceptCallback_01(ActionResultVO ar) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Trashed);
            switch (D.Cards[ar.SelectedUniqueCardId].CardColor) {
                case CardColor_Enum.Blue: { ar.CrystalGreen(1); ar.CrystalWhite(1); ar.CrystalRed(1); break; }
                case CardColor_Enum.Green: { ar.CrystalBlue(1); ar.CrystalWhite(1); ar.CrystalRed(1); break; }
                case CardColor_Enum.White: { ar.CrystalBlue(1); ar.CrystalGreen(1); ar.CrystalRed(1); break; }
                case CardColor_Enum.Red: { ar.CrystalBlue(1); ar.CrystalGreen(1); ar.CrystalWhite(1); break; }
            }
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = base.IsSelectionAllowed(card, cardHolder, ar);
            if (msg.Length == 0) {
                if (card.CardType == CardType_Enum.Basic || card.CardType == CardType_Enum.Advanced) {
                } else {
                    msg = "You can only play Action cards!";
                }
            }
            return msg;
        }
    }
}
