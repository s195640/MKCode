namespace cna {
    public partial class EndlessBagofGoldVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.ActionInfluence(4);
            ar.Fame(2);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ActionInfluence(9);
            ar.Fame(3);
            return ar;
        }
    }
}
