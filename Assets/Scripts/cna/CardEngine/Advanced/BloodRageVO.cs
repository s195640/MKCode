using cna.poo;
namespace cna {
    public partial class BloodRageVO : CardActionVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Attack 2", Image_Enum.I_attack),
                new OptionVO("Attack 5", Image_Enum.I_blood)
                );
        }

        public void acceptCallback_00(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleAttack(new AttackData(2));
                    break;
                }
                case 1: {
                    ar.BattleAttack(new AttackData(5));
                    ar.AddWound(1);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Attack 4", Image_Enum.I_attack),
                new OptionVO("Attack 9", Image_Enum.I_blood)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleAttack(new AttackData(4 + ar.CardModifier));
                    break;
                }
                case 1: {
                    ar.BattleAttack(new AttackData(9 + ar.CardModifier));
                    ar.AddWound(1);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
