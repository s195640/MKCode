using cna.poo;
namespace cna {
    public partial class BannerofGloryVO : CardArtifactVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddBanner(ar.SelectedUniqueCardId, ar.UniqueCardId);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            }
            return msg;
        }


        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CT_BannerOfGlory);
            return ar;
        }
    }
}
