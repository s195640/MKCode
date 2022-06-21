namespace cna {
    public partial class EndlessBagofGoldVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.ActionInfluence(4);
            ar.Fame(2);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ActionInfluence(9);
            ar.Fame(3);
            return ar;
        }
    }
}
