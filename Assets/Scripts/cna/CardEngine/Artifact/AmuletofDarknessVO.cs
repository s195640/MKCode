using cna.poo;
namespace cna {
    public partial class AmuletofDarknessVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.ManaBlack(1);
            if (D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfDarkness);
            }
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ManaBlack(3);
            if (D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfDarkness);
            }
            return ar;
        }
    }
}
