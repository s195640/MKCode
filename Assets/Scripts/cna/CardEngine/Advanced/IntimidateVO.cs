using cna.poo;
namespace cna {
    public partial class IntimidateVO : CardActionVO {

        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Influence 4", Image_Enum.I_influencegrey),
                new OptionVO("Attack 3", Image_Enum.I_attack)
                );
        }

        public void acceptCallback_00(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.ActionInfluence(4);
                    break;
                }
                case 1: {
                    ar.BattleAttack(new AttackData(3));
                    break;
                }
            }
            ar.Rep(-1);
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Influence 8", Image_Enum.I_influencegrey),
                new OptionVO("Attack 7", Image_Enum.I_attack)
                );
        }

        public void acceptCallback_01(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.ActionInfluence(8 + ar.CardModifier);
                    break;
                }
                case 1: {
                    ar.BattleAttack(new AttackData(7 + ar.CardModifier));
                    break;
                }
            }
            ar.Rep(-2);
            ar.FinishCallback(ar);
        }
    }
}
