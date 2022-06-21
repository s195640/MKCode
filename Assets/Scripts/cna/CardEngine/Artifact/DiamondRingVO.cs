using cna.poo;
namespace cna {
    public partial class DiamondRingVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalWhite(1);
            ar.ManaWhite(1);
            ar.Fame(1);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ManaWhite(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_DiamondRing);
            return ar;
        }
    }
}
