using cna.poo;
namespace cna {
    public partial class FireMagesVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 3;
            ar.BattleRange(a);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 6;
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            return ar;
        }

        public override GameAPI ActionValid_02(GameAPI ar) {
            ar.CrystalRed(1);
            ar.ManaRed(1);
            return ar;
        }
    }
}
