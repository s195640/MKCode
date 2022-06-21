using cna.poo;
namespace cna {
    public partial class BannerofGloryVO : CardArtifactVO {
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
            ar.AddGameEffect(GameEffect_Enum.CT_BannerOfGlory);
            return ar;
        }
    }
}
