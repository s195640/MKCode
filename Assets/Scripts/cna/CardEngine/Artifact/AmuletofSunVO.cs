using cna.poo;
namespace cna {
    public partial class AmuletofSunVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.ManaGold(1);
            if (!D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfTheSun);
            }
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ManaGold(3);
            if (!D.Scenario.isDay) {
                ar.AddGameEffect(GameEffect_Enum.CT_AmuletOfTheSun);
            }
            return ar;
        }
    }
}
