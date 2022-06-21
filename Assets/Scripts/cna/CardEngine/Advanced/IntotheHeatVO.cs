using cna.poo;
namespace cna {
    public partial class IntotheHeatVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.AC_IntoTheHeat01);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.AC_IntoTheHeat02);
            return ar;
        }
    }
}
