using cna.poo;
namespace cna {
    public partial class GoldenGrailVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.Healing(2);
            ar.Fame(2);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.Healing(6);
            ar.AddGameEffect(GameEffect_Enum.CT_GoldenGrail);
            return ar;
        }
    }
}
