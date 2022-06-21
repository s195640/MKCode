using cna.poo;
namespace cna {
    public partial class MaximalEffectVO : CardActionVO {

        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }
        public void acceptCallback_00(GameAPI ar) {
            selectedCard = (CardActionVO)D.Cards[ar.SelectedUniqueCardId];
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Trashed);
            ar.FinishCallback = finishCard01;
            selectedCard.ActionPaymentComplete_00(ar);
        }

        private CardActionVO selectedCard;

        public void finishCard01(GameAPI ar) {
            ar.FinishCallback = finishCard02;
            selectedCard.ActionPaymentComplete_00(ar);
        }
        public void finishCard02(GameAPI ar) {
            ar.FinishCallback = finishCard03;
            selectedCard.ActionPaymentComplete_00(ar);
        }
        public void finishCard03(GameAPI ar) {
            ActionFinish_00(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = base.IsSelectionAllowed(card, cardHolder, ar);
            if (msg.Length == 0) {
                GameAPI ar2 = new GameAPI(card.UniqueId, CardState_Enum.NA, 0);
                checkAllowedToUse(ar2);
                if (!ar2.Status) {
                    return ar2.ErrorMsg;
                }
            }
            return msg;
        }

        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_01);
        }

        public void acceptCallback_01(GameAPI ar) {
            selectedCard = (CardActionVO)D.Cards[ar.SelectedUniqueCardId];
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Trashed);
            ar.FinishCallback = finishCard11;
            selectedCard.ActionPaymentComplete_01(ar);
        }
        public void finishCard11(GameAPI ar) {
            ar.FinishCallback = finishCard12;
            selectedCard.ActionPaymentComplete_01(ar);
        }

        public void finishCard12(GameAPI ar) {
            ActionFinish_01(ar);
        }
    }
}
