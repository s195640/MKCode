using cna.poo;
namespace cna {
    public partial class BLUE_ColdSwordsmanshipVO : CardSkillVO {

        public override void ActionPaymentComplete_00(GameAPI ar) {
            switch (ar.P.Battle.BattlePhase) {
                case BattlePhase_Enum.Attack: {
                    ar.SelectOptions(acceptCallback_00,
                        new OptionVO("Attack 2", Image_Enum.I_attack),
                        new OptionVO("Cold Attack 2", Image_Enum.I_attack)
                        );
                    break;
                }
            }
        }

        public void acceptCallback_00(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleAttack(new AttackData(2));
                    break;
                }
                case 1: {
                    AttackData a = new AttackData();
                    a.Cold = 2;
                    ar.BattleAttack(a);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
