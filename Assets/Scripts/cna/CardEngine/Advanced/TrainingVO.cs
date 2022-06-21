namespace cna {
    public partial class TrainingVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            //ar.AddGameEffect(GameEffect_Enum.CS_Learning01, 0);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            //ar.AddGameEffect(GameEffect_Enum.CS_Learning02, 0);
            return ar;
        }
    }
}
