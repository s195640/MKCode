using cna.poo;
namespace cna {
    public partial class FireBoltVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalRed(1);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire += (3 + ar.CardModifier);
            ar.BattleRange(a);
            return ar;
        }
    }
}
