using cna.poo;
namespace cna {
    public partial class SwiftBoltVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalWhite(1);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleRange(new AttackData(4 + ar.CardModifier));
            return ar;
        }
    }
}
