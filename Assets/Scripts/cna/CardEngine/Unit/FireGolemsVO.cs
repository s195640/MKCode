using cna.poo;
namespace cna {
    public partial class FireGolemsVO : CardUnitVO {

        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(3));
            } else {
                ar.BattleAttack(new AttackData(3));
            }
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 4;
            ar.BattleRange(a);
            return ar;
        }
    }
}
