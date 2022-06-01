using cna.poo;
namespace cna {
    public partial class SapphireRingVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalBlue(1);
            ar.ManaBlue(1);
            ar.Fame(1);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaBlue(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_SapphireRing);
            return ar;
        }
    }
}
