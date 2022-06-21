using cna.poo;
namespace cna {
    public partial class RED_DarkFireMagicVO : CardSkillVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Red Mana", Image_Enum.I_mana_red),
                new OptionVO("Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.CrystalRed(1);
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
    }
}
