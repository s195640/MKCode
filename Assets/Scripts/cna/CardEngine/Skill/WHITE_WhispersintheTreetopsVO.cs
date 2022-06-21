namespace cna {
    public partial class WHITE_WhispersintheTreetopsVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.CrystalWhite(1);
            ar.ManaGreen(1);
            return ar;
        }
    }
}
