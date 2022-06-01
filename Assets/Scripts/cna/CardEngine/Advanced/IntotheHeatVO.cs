using cna.poo;
namespace cna {
    public partial class IntotheHeatVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.AC_IntoTheHeat01);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.AC_IntoTheHeat02);
            return ar;
        }
    }
}
