using cna.poo;
namespace cna {
    public partial class BannerofProtectionVO : CardArtifactVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.AddBanner(ar.SelectedUniqueCardId, ar.UniqueCardId);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            }
            return msg;
        }


        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.P.Deck.Hand.ForEach(c => {
                CardVO card = D.Cards[c];
                if (card.CardType == CardType_Enum.Wound && !ar.P.Deck.State.ContainsKey(c)) {
                    ar.AddCardState(c, CardState_Enum.Trashed);
                }
            });
            return ar;
        }
    }
}
