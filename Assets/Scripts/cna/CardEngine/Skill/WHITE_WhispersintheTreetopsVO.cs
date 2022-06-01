namespace cna {
    public partial class WHITE_WhispersintheTreetopsVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.CrystalWhite(1);
            ar.ManaGreen(1);
            return ar;
        }
    }
}
