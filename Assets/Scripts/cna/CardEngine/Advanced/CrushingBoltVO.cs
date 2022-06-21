using cna.poo;
namespace cna {
    public partial class CrushingBoltVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalGreen(1);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleSiege(new AttackData(3 + ar.CardModifier));
            return ar;
        }
    }
}
