using cna.poo;
namespace cna {
    public partial class AmuletofSunVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.ManaGold(1);
            if (!D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfTheSun);
            }
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaGold(3);
            if (!D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfTheSun);
            }
            return ar;
        }
    }
}
