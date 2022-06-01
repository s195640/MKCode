using cna.poo;
namespace cna {
    public partial class DiamondRingVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalWhite(1);
            ar.ManaWhite(1);
            ar.Fame(1);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaWhite(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_DiamondRing);
            return ar;
        }
    }
}
