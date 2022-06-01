namespace cna {
    public partial class TrainingVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            //ar.AddGameEffect(GameEffect_Enum.CS_Learning01, 0);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            //ar.AddGameEffect(GameEffect_Enum.CS_Learning02, 0);
            return ar;
        }
    }
}
