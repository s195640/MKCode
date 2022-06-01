using cna.poo;
namespace cna {
    public partial class RED_HotSwordsmanshipVO : CardSkillVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            switch (ar.LocalPlayer.Battle.BattlePhase) {
                case BattlePhase_Enum.Attack: {
                    ar.SelectOptions(acceptCallback_00,
                        new OptionVO("Attack 2", Image_Enum.I_attack),
                        new OptionVO("Fire Attack 2", Image_Enum.I_attack)
                        );
                    break;
                }
            }
        }

        public void acceptCallback_00(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleAttack(new AttackData(2));
                    break;
                }
                case 1: {
                    AttackData a = new AttackData();
                    a.Fire = 2;
                    ar.BattleAttack(a);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
