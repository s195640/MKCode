using cna.poo;
namespace cna {
    public partial class GREEN_FreezingPowerVO : CardSkillVO {

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            switch (ar.LocalPlayer.Battle.BattlePhase) {
                case BattlePhase_Enum.RangeSiege:
                case BattlePhase_Enum.Attack: {
                    ar.SelectOptions(acceptCallback_00,
                        new OptionVO("Siege 1", Image_Enum.I_catapult),
                        new OptionVO("Cold Siege 1", Image_Enum.I_catapult)
                        );
                    break;
                }
            }
        }

        public void acceptCallback_00(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleSiege(new AttackData(1));
                    break;
                }
                case 1: {
                    AttackData a = new AttackData();
                    a.Cold = 1;
                    ar.BattleSiege(a);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
