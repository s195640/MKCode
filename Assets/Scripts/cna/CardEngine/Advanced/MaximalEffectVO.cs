using cna.poo;
namespace cna {
    public partial class MaximalEffectVO : CardActionVO {

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }
        public void acceptCallback_00(ActionResultVO ar) {
            selectedCard = (CardActionVO)D.Cards[ar.SelectedUniqueCardId];
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Trashed);
            ar.FinishCallback = finishCard01;
            selectedCard.ActionPaymentComplete_00(ar);
        }

        private CardActionVO selectedCard;

        public void finishCard01(ActionResultVO ar) {
            ar.FinishCallback = finishCard02;
            selectedCard.ActionPaymentComplete_00(ar);
        }
        public void finishCard02(ActionResultVO ar) {
            ar.FinishCallback = finishCard03;
            selectedCard.ActionPaymentComplete_00(ar);
        }
        public void finishCard03(ActionResultVO ar) {
            ActionFinish_00(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = base.IsSelectionAllowed(card, cardHolder, ar);
            if (msg.Length == 0) {
                ActionResultVO ar2 = new ActionResultVO(card.UniqueId, CardState_Enum.NA, 0);
                checkAllowedToUse(ar2);
                if (!ar2.Status) {
                    return ar2.ErrorMsg;
                }
            }
            return msg;
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_01);
        }

        public void acceptCallback_01(ActionResultVO ar) {
            selectedCard = (CardActionVO)D.Cards[ar.SelectedUniqueCardId];
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Trashed);
            ar.FinishCallback = finishCard11;
            selectedCard.ActionPaymentComplete_01(ar);
        }
        public void finishCard11(ActionResultVO ar) {
            ar.FinishCallback = finishCard12;
            selectedCard.ActionPaymentComplete_01(ar);
        }

        public void finishCard12(ActionResultVO ar) {
            ActionFinish_01(ar);
        }
    }
}
