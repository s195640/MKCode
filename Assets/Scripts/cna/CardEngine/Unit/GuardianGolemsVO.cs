using cna.poo;

namespace cna {
    public partial class GuardianGolemsVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(2));
            } else {
                ar.BattleAttack(new AttackData(2));
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 4;
            ar.BattleBlock(a);
            return ar;
        }
        public override GameAPI ActionValid_02(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold = 4;
            ar.BattleBlock(a);
            return ar;
        }
    }
}
