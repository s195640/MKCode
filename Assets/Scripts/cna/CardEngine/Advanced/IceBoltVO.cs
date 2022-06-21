using cna.poo;
namespace cna {
    public partial class IceBoltVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalBlue(1);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold += (3 + ar.CardModifier);
            ar.BattleRange(a);
            return ar;
        }
    }
}
