using cna.poo;
namespace cna {
    public partial class EmeraldRingVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalGreen(1);
            ar.ManaGreen(1);
            ar.Fame(1);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaGreen(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_EmeraldRing);
            return ar;
        }
    }
}
