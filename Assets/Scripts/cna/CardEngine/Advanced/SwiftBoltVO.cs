using cna.poo;
namespace cna {
    public partial class SwiftBoltVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalWhite(1);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleRange(new AttackData(4 + ar.CardModifier));
            return ar;
        }
    }
}
