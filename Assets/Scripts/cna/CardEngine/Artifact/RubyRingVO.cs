using cna.poo;
namespace cna {
    public partial class RubyRingVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalRed(1);
            ar.ManaRed(1);
            ar.Fame(1);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaRed(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_RubyRing);
            return ar;
        }
    }
}
