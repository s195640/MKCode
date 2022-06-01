using cna.poo;
namespace cna {
    public partial class EndlessGemPouchVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.AddCrystal((Crystal_Enum)UnityEngine.Random.Range(2, 6));
            ar.AddCrystal((Crystal_Enum)UnityEngine.Random.Range(2, 6));
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ManaBlue(1);
            ar.ManaGreen(1);
            ar.ManaRed(1);
            ar.ManaWhite(1);
            if (D.Scenario.isDay) {
                ar.ManaGold(1);
            } else {
                ar.ManaBlack(1);
            }
            return ar;
        }
    }
}
