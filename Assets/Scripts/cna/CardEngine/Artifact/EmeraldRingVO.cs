using cna.poo;
namespace cna {
    public partial class EmeraldRingVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalGreen(1);
            ar.ManaGreen(1);
            ar.Fame(1);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ManaGreen(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_EmeraldRing);
            return ar;
        }
    }
}
