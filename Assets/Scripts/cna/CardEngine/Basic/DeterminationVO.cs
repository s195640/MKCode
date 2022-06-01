using cna.poo;

namespace cna {
    public partial class DeterminationVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Attack) {
                ar.BattleAttack(new AttackData(2));
            } else {
                ar.BattleBlock(new AttackData(2));
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleBlock(new AttackData(5 + ar.CardModifier));
            return ar;
        }
    }
}
