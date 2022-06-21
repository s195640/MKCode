using cna.poo;

namespace cna {
    public partial class UtemCrossbowmenVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(3));
            } else {
                ar.BattleAttack(new AttackData(3));
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleRange(new AttackData(2));
            return ar;
        }
    }
}
