using cna.poo;
namespace cna {
    public partial class GoldenGrailVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.Healing(2);
            ar.Fame(2);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.Healing(6);
            ar.AddGameEffect(GameEffect_Enum.CT_GoldenGrail);
            return ar;
        }
    }
}
