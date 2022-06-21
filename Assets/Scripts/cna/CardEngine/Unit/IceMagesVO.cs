using cna.poo;
namespace cna {
    public partial class IceMagesVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold = 4;
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold = 4;
            ar.BattleSiege(a);
            return ar;
        }

        public override GameAPI ActionValid_02(GameAPI ar) {
            ar.CrystalBlue(1);
            ar.ManaBlue(1);
            return ar;
        }
    }
}
