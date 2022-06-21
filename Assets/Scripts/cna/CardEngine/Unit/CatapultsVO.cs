using cna.poo;
namespace cna {
    public partial class CatapultsVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Physical = 3;
            ar.BattleSiege(a);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 5;
            ar.BattleSiege(a);
            return ar;
        }

        public override GameAPI ActionValid_02(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold = 5;
            ar.BattleSiege(a);
            return ar;
        }
    }
}
