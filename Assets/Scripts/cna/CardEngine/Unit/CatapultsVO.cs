using cna.poo;
namespace cna {
    public partial class CatapultsVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Physical = 3;
            ar.BattleSiege(a);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 5;
            ar.BattleSiege(a);
            return ar;
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Cold = 5;
            ar.BattleSiege(a);
            return ar;
        }
    }
}
