using cna.poo;
namespace cna {
    public partial class BLUE_ShieldMasteryVO : CardSkillVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_00,
                        new OptionVO("Block 3", Image_Enum.I_shield),
                        new OptionVO("Cold Block 2", Image_Enum.I_shield),
                        new OptionVO("Fire Block 2", Image_Enum.I_shield)
                        );
        }

        public void acceptCallback_00(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleBlock(new AttackData(3));
                    break;
                }
                case 1: {
                    AttackData a = new AttackData();
                    a.Cold = 2;
                    ar.BattleBlock(a);
                    break;
                }
                case 2: {
                    AttackData a = new AttackData();
                    a.Fire = 2;
                    ar.BattleBlock(a);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
