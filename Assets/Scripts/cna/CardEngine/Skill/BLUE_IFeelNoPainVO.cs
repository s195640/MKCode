using cna.poo;
namespace cna {
    public partial class BLUE_IFeelNoPainVO : CardSkillVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
            ar.DrawCard(1, ar.FinishCallback);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = base.IsSelectionAllowed(card, cardHolder, ar);
            if (msg.Length == 0) {
                if (card.CardType != CardType_Enum.Wound) {
                    msg = "You must select a wound from your hand!";
                }
            }
            return msg;
        }
    }
}
