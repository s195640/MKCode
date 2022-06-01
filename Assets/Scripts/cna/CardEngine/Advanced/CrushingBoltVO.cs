using cna.poo;
namespace cna {
    public partial class CrushingBoltVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalGreen(1);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleSiege(new AttackData(3 + ar.CardModifier));
            return ar;
        }
    }
}
