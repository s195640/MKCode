using cna.poo;
namespace cna {
    public partial class RED_InvocationVO : CardSkillVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }
        public void acceptCallback_00(GameAPI ar) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
            CardVO card = D.Cards[ar.SelectedUniqueCardId];
            if (card.CardType == CardType_Enum.Wound) {
                ar.SelectOptions(acceptCallback_01,
                   new OptionVO("Red Mana", Image_Enum.I_mana_red),
                   new OptionVO("Black Mana", Image_Enum.I_mana_black)
                   );
            } else {
                ar.SelectOptions(acceptCallback_02,
                   new OptionVO("Green Mana", Image_Enum.I_mana_green),
                   new OptionVO("White Mana", Image_Enum.I_mana_white)
                   );
            }
        }

        public void acceptCallback_01(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.ManaRed(1);
                    break;
                }
                case 1: {
                    ar.ManaBlack(1);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }

        public void acceptCallback_02(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.ManaGreen(1);
                    break;
                }
                case 1: {
                    ar.ManaWhite(1);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
