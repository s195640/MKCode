using cna.poo;
namespace cna {
    public partial class RubyRingVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalRed(1);
            ar.ManaRed(1);
            ar.Fame(1);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ManaRed(49);
            ar.ManaBlack(49);
            ar.AddGameEffect(GameEffect_Enum.CT_RubyRing);
            return ar;
        }
    }
}
