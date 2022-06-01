using cna.poo;
namespace cna {
    public partial class FireballVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 5;
            ar.BattleRange(a);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 8;
            ar.BattleSiege(a);
            ar.AddWound(1);
            return ar;
        }
    }
}
