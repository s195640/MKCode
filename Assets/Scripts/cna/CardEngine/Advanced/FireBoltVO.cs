using cna.poo;
namespace cna {
    public partial class FireBoltVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalRed(1);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire += (3 + ar.CardModifier);
            ar.BattleRange(a);
            return ar;
        }
    }
}
