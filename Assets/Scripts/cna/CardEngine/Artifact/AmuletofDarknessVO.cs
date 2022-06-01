using cna.poo;
namespace cna {
    public partial class AmuletofDarknessVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.ManaBlack(1);
            if (D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfDarkness);
            }
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaBlack(3);
            if (D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfDarkness);
            }
            return ar;
        }
    }
}
