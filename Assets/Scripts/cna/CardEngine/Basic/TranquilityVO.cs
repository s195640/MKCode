using cna.poo;

namespace cna {
    public partial class TranquilityVO : CardActionVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Healing", Image_Enum.I_healHand),
                new OptionVO("Draw Card", Image_Enum.I_cardBackRounded)
                );
        }

        public void acceptCallback_00(ActionResultVO ar) {
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
            }
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Healing", Image_Enum.I_healHand),
                new OptionVO("Draw Card", Image_Enum.I_cardBackRounded)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
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
            }
        }
    }
}
